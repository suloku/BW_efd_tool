/*
 * Created by SharpDevelop.
 * User: suloku
 * Date: 17/10/2015
 * Time: 16:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace BW_Entralink_Forest_tool
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		string forestfile;
		byte[] savebuffer = new byte[524288];
		byte[] forestbuffer = new byte[2304];

		//adapted from Gocario's PHBank (www.github.com/gocario/phbank)
		byte[] ccitt16(byte[] data)
		// --------------------------------------------------
		{
			int len = data.Length;
			UInt16 crc = 0xFFFF;
		
			for (UInt32 i = 0; i < len; i++)
			{
				crc ^= ((UInt16)((data[i] << 8)&0x0000FFFF));
		
				for (UInt32 j = 0; j < 0x8; j++)
				{
					if ((crc & 0x8000) > 0)
						crc = (UInt16)(((UInt16)((crc << 1)&0x0000FFFF ) ^ 0x1021) &0x0000FFFF);
					else
						crc <<= 1;
				}
			}
		
			return BitConverter.GetBytes(crc);
		}

		/// <summary>
		/// Reads data into a complete array, throwing an EndOfStreamException
		/// if the stream runs out of data first, or if an IOException
		/// naturally occurs.
		/// </summary>
		/// <param name="stream">The stream to read data from</param>
		/// <param name="data">The array to read bytes into. The array
		/// will be completely filled from the stream, so an appropriate
		/// size must be given.</param>
		public static void ReadWholeArray (Stream stream, byte[] data)
		{
		    int offset=0;
		    int remaining = data.Length;
		    while (remaining > 0)
		    {
		        int read = stream.Read(data, offset, remaining);
		        if (read <= 0)
		            throw new EndOfStreamException 
		                (String.Format("End of stream reached with {0} bytes left to read", remaining));
		        remaining -= read;
		        offset += read;
		    }
		}
		private void PDR_read_data()
		{
	            System.IO.FileStream saveFile;
	            saveFile = new FileStream(savegamename.Text, FileMode.Open);
	            if (saveFile.Length != 524288){
	            	savegamename.Text = "";
	            	MessageBox.Show("Invalid file length", "Error");
	            	return;
	            }
	            ReadWholeArray(saveFile, savebuffer);
	            saveFile.Close();
		}
		private void PDR_get_data()
        {
            OpenFileDialog openFD = new OpenFileDialog();
            //openFD.InitialDirectory = "c:\\";
            openFD.Filter = "NDS save data|*.sav;*.dsv|All Files (*.*)|*.*";
            if (openFD.ShowDialog() == DialogResult.OK)
            {
                #region filename
                savegamename.Text = openFD.FileName;
                #endregion
                PDR_read_data();
            }
            
        }
		private void PDR_save_data()
		{	if (savegamename.Text.Length < 1) return;
            SaveFileDialog saveFD = new SaveFileDialog();
            //saveFD.InitialDirectory = "c:\\";
            saveFD.Filter = "NDS save data|*.sav;*.dsv|All Files (*.*)|*.*";
            if (saveFD.ShowDialog() == DialogResult.OK)
            {
	            System.IO.FileStream saveFile;
	            saveFile = new FileStream(saveFD.FileName, FileMode.Create);            
	            //Write file
	            saveFile.Write(savebuffer, 0, savebuffer.Length);
	            saveFile.Close();
	            MessageBox.Show("File Saved.", "Save file");
            }
		}
		private void PDR_dump_forest_data()
		{	if (savegamename.Text.Length < 1) return;
            SaveFileDialog saveFD = new SaveFileDialog();
            //saveFD.InitialDirectory = "c:\\";
            saveFD.Filter = "Entralink Forest Data|*.efd|All Files (*.*)|*.*";
            if (saveFD.ShowDialog() == DialogResult.OK)
            {
	            System.IO.FileStream saveFile;
	            saveFile = new FileStream(saveFD.FileName, FileMode.Create);            
	            //Write file
	            saveFile.Write(savebuffer, 0x22C00, 0x900);
	            saveFile.Close();
	            MessageBox.Show("Entralink forest data dumped to:\r"+saveFD.FileName+".", "Dump Entralink Forest Data");
            }
		}
		private void PDR_read_forest_data()
		{
	            System.IO.FileStream saveFile;
	            saveFile = new FileStream(forestfile, FileMode.Open);
	            if (saveFile.Length != 0x900){
	            	//forestfile = "";
	            	MessageBox.Show("Invalid file length", "Error");
	            	return;
	            }
	            ReadWholeArray(saveFile, forestbuffer);
	            saveFile.Close();
	            PDR_injectNsave();
		}
		private void PDR_get_forest_data()
        {
            OpenFileDialog openFD = new OpenFileDialog();
            //openFD.InitialDirectory = "c:\\";
            openFD.Filter = "Entralink Forest Data|*.efd|All Files (*.*)|*.*";
            if (openFD.ShowDialog() == DialogResult.OK)
            {
                #region filename
                forestfile = openFD.FileName;
                #endregion
                PDR_read_forest_data();
            }
            
        }
		private void PDR_injectNsave()
		{
			//Put new forest in both save file slots
			Array.Copy(forestbuffer, 0, savebuffer, 0x22C00, 0x900);//slot 1
			Array.Copy(forestbuffer, 0, savebuffer, 0x22C00+0x24000, 0x900);//slot 2
			//Get checksum for new forest data block
			Int32 pos = 0x900-1;
			for (pos = 0x900-1; pos > 0; pos--){
				if (forestbuffer[pos] != 0x00){
					//MessageBox.Show( BitConverter.ToUInt16(forestbuffer, pos-1).ToString());
					if (pos > 0) pos --;
					break;
				}
			}
			//Put it into checksum table
			Array.Copy(forestbuffer, pos, savebuffer, 0x23F00+0x7A, 2);
			Array.Copy(forestbuffer, pos, savebuffer, 0x23F00+0x7A+0x24000, 2); // Slot 2
			//MessageBox.Show(BitConverter.ToUInt16(forestbuffer, pos).ToString());

			//Recalculate checksum table's checksum
			byte[] checktable = new byte[0x8C];
			Array.Copy(savebuffer, 0x23F00, checktable, 0, 0x8C);
			byte[] tablecrcsum = new byte[2];
			//tablecrcsum = checksum.ComputeChecksumBytes(checktable);
			tablecrcsum = ccitt16(checktable);
			//MessageBox.Show(BitConverter.ToUInt16(tablecrcsum, 0).ToString());
			//Put new checksum in savefile
			Array.Copy(tablecrcsum, 0, savebuffer, 0x23F9A, 2);
			Array.Copy(tablecrcsum, 0, savebuffer, 0x23F9A+0x24000, 2); // Slot 2
			//Write Data
			PDR_save_data();
		}
		void Button1Click(object sender, EventArgs e)
		{
			PDR_get_data();
		}
		void Dump_butClick(object sender, EventArgs e)
		{
			if (savegamename.Text.Length < 1) return;
			PDR_dump_forest_data();
		}
		void SavegamenameTextChanged(object sender, EventArgs e)
		{
			if (savegamename.Text.Length > 0){
				dump_but.Enabled = true;
				inject_but.Enabled = true;
			}else{
				dump_but.Enabled = false;
				inject_but.Enabled = false;
			}

		}
		void Inject_butClick(object sender, EventArgs e)
		{
			PDR_get_forest_data();
		}

	}
}
