/*
 * Created by SharpDevelop.
 * User: suloku
 * Date: 17/10/2015
 * Time: 16:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace BW_Entralink_Forest_tool
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button loadsave;
		private System.Windows.Forms.TextBox savegamename;
		private System.Windows.Forms.Button dump_but;
		private System.Windows.Forms.Button inject_but;
		private System.Windows.Forms.Button dump_dec_but;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.loadsave = new System.Windows.Forms.Button();
			this.savegamename = new System.Windows.Forms.TextBox();
			this.dump_but = new System.Windows.Forms.Button();
			this.inject_but = new System.Windows.Forms.Button();
			this.dump_dec_but = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// loadsave
			// 
			this.loadsave.Location = new System.Drawing.Point(93, 12);
			this.loadsave.Name = "loadsave";
			this.loadsave.Size = new System.Drawing.Size(162, 23);
			this.loadsave.TabIndex = 0;
			this.loadsave.Text = "Load B/W Savegame";
			this.loadsave.UseVisualStyleBackColor = true;
			this.loadsave.Click += new System.EventHandler(this.Button1Click);
			// 
			// savegamename
			// 
			this.savegamename.Location = new System.Drawing.Point(12, 41);
			this.savegamename.Name = "savegamename";
			this.savegamename.Size = new System.Drawing.Size(330, 20);
			this.savegamename.TabIndex = 1;
			this.savegamename.TextChanged += new System.EventHandler(this.SavegamenameTextChanged);
			// 
			// dump_but
			// 
			this.dump_but.Enabled = false;
			this.dump_but.Location = new System.Drawing.Point(12, 67);
			this.dump_but.Name = "dump_but";
			this.dump_but.Size = new System.Drawing.Size(162, 23);
			this.dump_but.TabIndex = 2;
			this.dump_but.Text = "Dump Entralink Forest Data";
			this.dump_but.UseVisualStyleBackColor = true;
			this.dump_but.Click += new System.EventHandler(this.Dump_butClick);
			// 
			// inject_but
			// 
			this.inject_but.Enabled = false;
			this.inject_but.Location = new System.Drawing.Point(180, 67);
			this.inject_but.Name = "inject_but";
			this.inject_but.Size = new System.Drawing.Size(162, 23);
			this.inject_but.TabIndex = 4;
			this.inject_but.Text = "Inject Entralink Forest Data";
			this.inject_but.UseVisualStyleBackColor = true;
			this.inject_but.Click += new System.EventHandler(this.Inject_butClick);
			// 
			// dump_dec_but
			// 
			this.dump_dec_but.Enabled = false;
			this.dump_dec_but.Location = new System.Drawing.Point(13, 97);
			this.dump_dec_but.Name = "dump_dec_but";
			this.dump_dec_but.Size = new System.Drawing.Size(161, 36);
			this.dump_dec_but.TabIndex = 5;
			this.dump_dec_but.Text = "Dump Entralink Forest Data (decrypted)";
			this.dump_dec_but.UseVisualStyleBackColor = true;
			this.dump_dec_but.Click += new System.EventHandler(this.Dump_dec_butClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(354, 136);
			this.Controls.Add(this.dump_dec_but);
			this.Controls.Add(this.savegamename);
			this.Controls.Add(this.dump_but);
			this.Controls.Add(this.loadsave);
			this.Controls.Add(this.inject_but);
			this.Name = "MainForm";
			this.Text = "BW Entralink Forest tool 0.1 by suloku";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
