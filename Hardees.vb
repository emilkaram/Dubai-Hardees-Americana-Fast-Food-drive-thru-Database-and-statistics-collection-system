using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Runtime.InteropServices;


namespace car
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            
            InitializeComponent();
        }

        OleDbConnection carcon;
        OleDbCommand carcomm;
        OleDbDataAdapter caradapt;
        DataTable cartable;


        private class PortAccess
        {

            [DllImport("inpout32.dll", EntryPoint = "Out32")]
            public static extern void Output(int address, int value);

            [DllImport("inpout32.dll", EntryPoint = "Inp32")]
            public static extern int Input(int address);

        }








        private void button1_Click(object sender, EventArgs e)
        {

            //int i = Convert.ToInt32();
            int i = dataGridView1.RowCount-1;
            i++;
            label1.Text = i.ToString();
     
            //string s = textBox2.Text;
            string s;
            DateTime CurrTime = DateTime.Now;
            
            s = "'" + CurrTime.ToString() + "'";

            textBox1.Text =Convert.ToString((Convert.ToInt32(label1.Text) - Convert.ToInt32(label2.Text)));

            carcomm = new OleDbCommand("Insert into table1(car,datein,timein,dateout,timeout,timeelapsed) Values(" + i + "," + s + "," + s + ",'Not Yet Out','Not Yet Out','Not Yet Out')", carcon);
            carcomm.ExecuteNonQuery();

            carcomm = new OleDbCommand("Select * from table1", carcon);

            caradapt = new OleDbDataAdapter();
            caradapt.SelectCommand = carcomm;
            cartable = new DataTable();
            caradapt.Fill(cartable);



            dataGridView1.DataSource = cartable;


        }

       
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

       

        

       

        private void Form1_Load_1(object sender, EventArgs e)
        {
            carcon = new OleDbConnection("Provider=Microsoft.jet.OLEDB.4.0;Data Source=car.mdb");
            carcon.Open();
            carcomm = new OleDbCommand("Select * from table1", carcon);
            caradapt = new OleDbDataAdapter();
            caradapt.SelectCommand = carcomm;
            cartable = new DataTable();
            caradapt.Fill(cartable);


            //textBox1.DataBindings.Add("text", cartable, "timein");

            dataGridView1.DataSource = cartable;

            label1.Text = (dataGridView1.RowCount - 1).ToString();
            label2.Text = (dataGridView1.RowCount-1).ToString();
            textBox1.Text = Convert.ToString((Convert.ToInt32(label1.Text) - Convert.ToInt32(label2.Text)));

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label1.Text) >Convert.ToInt32(label2.Text))
            {

            int o = Convert.ToInt32(label2.Text);

            DateTime CurrTime = DateTime.Now;

            string t = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[o].Cells[2].Value));

            //--------------------
                string tw = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[o].Cells[3].Value));
            //---------------
                
            o++;
            label2.Text = o.ToString();

            string s;
            
            s = "'" + CurrTime.ToString() + "'";

            string oo = o.ToString();

            
            carcomm = new OleDbCommand("Update table1 set timeout= "+s+"  where (car ='"+oo+"')", carcon);
            carcomm.ExecuteNonQuery();

            carcomm = new OleDbCommand("Update table1 set dateout= " + s + "  where (car ='" + oo + "')", carcon);
            carcomm.ExecuteNonQuery();

            carcomm = new OleDbCommand("Update table1 set timeelapsed ='"+t+"'  where (car ='" + oo + "')", carcon);
            carcomm.ExecuteNonQuery();

                //--------------------------

            carcomm = new OleDbCommand("Update table1 set timespentatwin ='" + tw + "'  where (car ='" + oo + "')", carcon);
            carcomm.ExecuteNonQuery();

                //------------------------------

             

            carcomm = new OleDbCommand("Select * from table1", carcon);

            caradapt = new OleDbDataAdapter();
            caradapt.SelectCommand = carcomm;
            cartable = new DataTable();
            caradapt.Fill(cartable);
            dataGridView1.DataSource = cartable;

            textBox1.Text = Convert.ToString((Convert.ToInt32(label1.Text) - Convert.ToInt32(label2.Text)));
            }

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //port scanning

           

            int y;
            y = PortAccess.Input(889);
            
             textBox7.Text = y.ToString();
             textBox7.Refresh();


            // end of port scanining



            //s1 in
            if (textBox7.Text == "0" || textBox7.Text == "16" || textBox7.Text == "32" || textBox7.Text == "48" || textBox7.Text == "64" || textBox7.Text == "80" || textBox7.Text == "96" || textBox7.Text == "112")
            {
                textBox3.Text = "1";
            }
            else
            {
                textBox3.Text = "0";
            }
                
                //s2in
                                if(textBox7.Text=="0"||textBox7.Text=="8"||textBox7.Text=="32"||textBox7.Text=="40"||textBox7.Text=="64"||textBox7.Text=="72"||textBox7.Text=="96"||textBox7.Text=="104")
            {
            textBox4.Text="1";
            }
            else
            {
                textBox4.Text="0";
                                }


             
                                    //s1out
                                if(textBox7.Text=="0"||textBox7.Text=="8"||textBox7.Text=="16"||textBox7.Text=="24"||textBox7.Text=="64"||textBox7.Text=="72"||textBox7.Text=="80"||textBox7.Text=="88")
            {
            textBox5.Text="1";
            }
            else
            {
                textBox5.Text="0";
                                }


                                    //s2out
                                    //s2in
                                if(textBox7.Text=="0"||textBox7.Text=="8"||textBox7.Text=="16"||textBox7.Text=="24"||textBox7.Text=="32"||textBox7.Text=="40"||textBox7.Text=="48"||textBox7.Text=="56")
            {
            textBox6.Text="1";
            }
            else
            {
                textBox6.Text="0";


                                }



                           

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label11.Text = label10.Text;
            label10.Text = label9.Text;
            label9.Text = textBox3.Text;


            if (label9.Text == "0" & label10.Text == "1" & label11.Text == "1")
            {
                label15.Text = "1";
            }
            else
            {
                label15.Text = "0";
            }



            label14.Text = label13.Text;
            label13.Text = label12.Text;
            label12.Text = textBox4.Text;

            if (label12.Text == "1" & label13.Text == "1" & label14.Text == "0")
            {
                label7.Text = "1";

            }
            else
            {
                label7.Text = "0";
            }




            if (label7.Text=="1" & label15.Text=="1")
            {
                label16.Text = "1";
                //int i = Convert.ToInt32();
                int i = dataGridView1.RowCount - 1;
                i++;
                label1.Text = i.ToString();

                //string s = textBox2.Text;
                string s;
                DateTime CurrTime = DateTime.Now;

                s = "'" + CurrTime.ToString() + "'";

                textBox1.Text = Convert.ToString((Convert.ToInt32(label1.Text) - Convert.ToInt32(label2.Text)));

                carcomm = new OleDbCommand("Insert into table1(car,datein,timein,dateout,timeout,timeelapsed) Values(" + i + "," + s + "," + s + ",'Not Yet Out','Not Yet Out','Not Yet Out')", carcon);
                carcomm.ExecuteNonQuery();

                carcomm = new OleDbCommand("Select * from table1", carcon);

                caradapt = new OleDbDataAdapter();
                caradapt.SelectCommand = carcomm;
                cartable = new DataTable();
                caradapt.Fill(cartable);



                dataGridView1.DataSource = cartable;

            }
            else
            {
                label16.Text="0";
            }

            //put here for reverse order cancelation

            //put here for reverse order cancelation
        
   

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label14.Text = label13.Text;
            label13.Text = label12.Text;
            label12.Text = textBox4.Text;

            if (label12.Text == "1" & label13.Text == "1" & label14.Text == "0")
            {
                label7.Text = "1";
                
            }
            else
            {
                label7.Text = "0";
            }



            label11.Text = label10.Text;
            label10.Text = label9.Text;
            label9.Text = textBox3.Text;


            if (label9.Text == "0" & label10.Text == "1" & label11.Text == "1")
            {
                label15.Text = "1";
            }
            else
            {
                label15.Text = "0";
            }






             if (label7.Text=="1" & label15.Text=="1")
            {
                label16.Text = "1";
                //int i = Convert.ToInt32();
                int i = dataGridView1.RowCount - 1;
                i++;
                label1.Text = i.ToString();

                //string s = textBox2.Text;
                string s;
                DateTime CurrTime = DateTime.Now;

                s = "'" + CurrTime.ToString() + "'";

                textBox1.Text = Convert.ToString((Convert.ToInt32(label1.Text) - Convert.ToInt32(label2.Text)));

                carcomm = new OleDbCommand("Insert into table1(car,datein,timein,dateout,timeout,timeelapsed) Values(" + i + "," + s + "," + s + ",'Not Yet Out','Not Yet Out','Not Yet Out')", carcon);
                carcomm.ExecuteNonQuery();

                carcomm = new OleDbCommand("Select * from table1", carcon);

                caradapt = new OleDbDataAdapter();
                caradapt.SelectCommand = carcomm;
                cartable = new DataTable();
                caradapt.Fill(cartable);



                dataGridView1.DataSource = cartable;

             
             }
            else
            {
                label16.Text="0";
            }

            
            //put here for reverse order cancelation

            //put here for reverse order cancelation

           
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {
        
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

            //for time at win
            //here the time at window start  

            if (Convert.ToInt32(label1.Text) > Convert.ToInt32(label2.Text))
            {
                int xx = Convert.ToInt32(label2.Text);
                DateTime CTime = DateTime.Now;
                string yy = Convert.ToString(CTime - Convert.ToDateTime(dataGridView1.Rows[xx].Cells[2].Value));
                xx++;
                label44.Text = xx.ToString();
                string zz;
                zz = "'" + CTime.ToString() + "'";
                string xxx = xx.ToString();
                carcomm = new OleDbCommand("Update table1 set timeatwin= " + zz + "  where (car ='" + xxx + "')", carcon);
                carcomm.ExecuteNonQuery();

                //------------
                carcomm = new OleDbCommand("Update table1 set timetoreachwin ='" + yy + "'  where (car ='" + xxx + "')", carcon);
                carcomm.ExecuteNonQuery();
                //------------

                carcomm = new OleDbCommand("Select * from table1", carcon);
                caradapt = new OleDbDataAdapter();
                caradapt.SelectCommand = carcomm;
                cartable = new DataTable();
                caradapt.Fill(cartable);
                dataGridView1.DataSource = cartable;

            }

            //timeatwindow end


            //end of win time
            
            
            label19.Text = label18.Text;
            label18.Text = label17.Text;
            label17.Text = textBox5.Text;

            if (label7.Text == "0" & label18.Text == "1" & label19.Text == "1")
            {
                label23.Text = "1";
            }
            else
            {
                label23.Text = "0";
            }




            label22.Text = label21.Text;
            label21.Text = label20.Text;
            label20.Text = textBox6.Text;

            if (label20.Text == "1" & label21.Text == "1" & label22.Text == "0")
            {
                label24.Text = "1";
            }
            else
            {
                label24.Text = "0";
            }






            if (label23.Text == "1" & label24.Text == "1")
            {
            
                
                label25.Text = "1";


            }
            else
            {
                label25.Text = "0";
            }

            if (label23.Text == "1" & label24.Text == "1")
            {
                //here
                if (Convert.ToInt32(label1.Text) > Convert.ToInt32(label2.Text))
                {

                    int o = Convert.ToInt32(label2.Text);

                    DateTime CurrTime = DateTime.Now;

                    string t = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[o].Cells[2].Value));
                    
                    //--------------------
                    string tw = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[o].Cells[3].Value));
                    //---------------

                    o++;
                    label2.Text = o.ToString();

                    string s;

                    s = "'" + CurrTime.ToString() + "'";

                    string oo = o.ToString();


                    carcomm = new OleDbCommand("Update table1 set timeout= " + s + "  where (car ='" + oo + "')", carcon);
                    carcomm.ExecuteNonQuery();

                    carcomm = new OleDbCommand("Update table1 set dateout= " + s + "  where (car ='" + oo + "')", carcon);
                    carcomm.ExecuteNonQuery();

                    carcomm = new OleDbCommand("Update table1 set timeelapsed ='" + t + "'  where (car ='" + oo + "')", carcon);
                    carcomm.ExecuteNonQuery();

                    //--------------------------

                    carcomm = new OleDbCommand("Update table1 set timespentatwin ='" + tw + "'  where (car ='" + oo + "')", carcon);
                    carcomm.ExecuteNonQuery();

                    //------------------------------

                    carcomm = new OleDbCommand("Select * from table1", carcon);

                    caradapt = new OleDbDataAdapter();
                    caradapt.SelectCommand = carcomm;
                    cartable = new DataTable();
                    caradapt.Fill(cartable);
                    dataGridView1.DataSource = cartable;

                    textBox1.Text = Convert.ToString((Convert.ToInt32(label1.Text) - Convert.ToInt32(label2.Text)));
            
                
                }

                
            }






        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            label22.Text = label21.Text;
            label21.Text = label20.Text;
            label20.Text = textBox6.Text;

            if (label20.Text == "1" & label21.Text == "1" & label22.Text == "0")
            {
                label24.Text = "1";
            }
            else
            {
                label24.Text = "0";
            }



            label19.Text = label18.Text;
            label18.Text = label17.Text;
            label17.Text = textBox5.Text;

            if (label7.Text == "0" & label18.Text == "1" & label19.Text == "1")
            {
                label23.Text = "1";
            }
            else
            {
                label23.Text = "0";
            }




            if (label23.Text == "1" & label24.Text == "1")
            {
                
                
                
                
                label25.Text = "1";


            }
            else
            {
                label25.Text = "0";
            }


            if (label23.Text == "1" & label24.Text == "1")
            {
                //here again

                if (Convert.ToInt32(label1.Text) > Convert.ToInt32(label2.Text))
                {

                    int o = Convert.ToInt32(label2.Text);

                    DateTime CurrTime = DateTime.Now;

                    string t = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[o].Cells[2].Value));

                    //--------------------
                    string tw = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[o].Cells[3].Value));
                    //---------------


                    o++;
                    label2.Text = o.ToString();

                    string s;

                    s = "'" + CurrTime.ToString() + "'";

                    string oo = o.ToString();


                    carcomm = new OleDbCommand("Update table1 set timeout= " + s + "  where (car ='" + oo + "')", carcon);
                    carcomm.ExecuteNonQuery();

                    carcomm = new OleDbCommand("Update table1 set dateout= " + s + "  where (car ='" + oo + "')", carcon);
                    carcomm.ExecuteNonQuery();

                    carcomm = new OleDbCommand("Update table1 set timeelapsed ='" + t + "'  where (car ='" + oo + "')", carcon);
                    carcomm.ExecuteNonQuery();

                    //--------------------------

                    carcomm = new OleDbCommand("Update table1 set timespentatwin ='" + tw + "'  where (car ='" + oo + "')", carcon);
                    carcomm.ExecuteNonQuery();

                    //------------------------------

                    carcomm = new OleDbCommand("Select * from table1", carcon);

                    caradapt = new OleDbDataAdapter();
                    caradapt.SelectCommand = carcomm;
                    cartable = new DataTable();
                    caradapt.Fill(cartable);
                    dataGridView1.DataSource = cartable;

                    textBox1.Text = Convert.ToString((Convert.ToInt32(label1.Text) - Convert.ToInt32(label2.Text)));
                }
            }






        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            {
            
                //port scan disp start



                int oo = Convert.ToInt32(label2.Text);
                DateTime CurrTime = DateTime.Now;

                if (textBox1.Text=="0")
                {
                   label31.Text="00:00";
                   label32.Text = "00:00";
                   label33.Text = "00:00";
                   label34.Text = "00:00";
                   label35.Text = "00:00";
                }


               if (textBox1.Text=="1")
               {
                                     
                   string tt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo].Cells[2].Value));
                   label31.Text = tt.ToString();

                   label32.Text = "00:00";
                   label33.Text = "00:00";
                   label34.Text = "00:00";
                   label35.Text = "00:00";


               
               }


               if (textBox1.Text == "2")
               {

                   string tt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo].Cells[2].Value));
                   label31.Text = tt.ToString();

                   string ttt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo+1].Cells[2].Value));
                   label32.Text = ttt.ToString();

                   label33.Text = "00:00";
                   label34.Text = "00:00";
                   label35.Text = "00:00";


               }


               if (textBox1.Text == "3")
               {

                   string tt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo].Cells[2].Value));
                   label31.Text = tt.ToString();

                   string ttt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo + 1].Cells[2].Value));
                   label32.Text = ttt.ToString();

                   string tttt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo + 2].Cells[2].Value));
                   label33.Text = tttt.ToString();
                   label34.Text = "00:00";
                   label35.Text = "00:00";


               }


               if (textBox1.Text == "4")
               {

                   string tt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo].Cells[2].Value));
                   label31.Text = tt.ToString();

                   string ttt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo + 1].Cells[2].Value));
                   label32.Text = ttt.ToString();

                   string tttt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo + 2].Cells[2].Value));
                   label33.Text = tttt.ToString();

                   string ttttt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo + 3].Cells[2].Value));
                   label34.Text = ttttt.ToString();

                   label35.Text = "00:00";


               }


               if (textBox1.Text== "5")
               {

                   string tt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo].Cells[2].Value));
                   label31.Text = tt.ToString();

                   string ttt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo + 1].Cells[2].Value));
                   label32.Text = ttt.ToString();

                   string tttt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo + 2].Cells[2].Value));
                   label33.Text = tttt.ToString();

                   string ttttt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo + 3].Cells[2].Value));
                   label34.Text = ttttt.ToString();

                   string tttttt = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[oo + 4].Cells[2].Value));
                   label35.Text = tttttt.ToString();




               }













                // port scan disp end
                

                
                
                //port scanning
                
                int ff=Convert.ToInt32(textBox2.Text);
                                
                ff++;

                if (ff >= 1000)
                {
                    ff = 0;

                }
                textBox2.Text = ff.ToString();


                int y;
                y = PortAccess.Input(889);

                textBox7.Text = y.ToString();
                textBox7.Refresh();


                // end of port scanining



                //s1 in
                if (textBox7.Text == "0" || textBox7.Text == "16" || textBox7.Text == "32" || textBox7.Text == "48" || textBox7.Text == "64" || textBox7.Text == "80" || textBox7.Text == "96" || textBox7.Text == "112")
                {
                    textBox3.Text = "1";
                }
                else
                {
                    textBox3.Text = "0";
                }

                //s2in
                if (textBox7.Text == "0" || textBox7.Text == "8" || textBox7.Text == "32" || textBox7.Text == "40" || textBox7.Text == "64" || textBox7.Text == "72" || textBox7.Text == "96" || textBox7.Text == "104")
                {
                    textBox4.Text = "1";
                }
                else
                {
                    textBox4.Text = "0";
                }



                //s1out
                if (textBox7.Text == "0" || textBox7.Text == "8" || textBox7.Text == "16" || textBox7.Text == "24" || textBox7.Text == "64" || textBox7.Text == "72" || textBox7.Text == "80" || textBox7.Text == "88")
                {
                    textBox5.Text = "1";


                   

                }
                else
                {
                    textBox5.Text = "0";
                }


                //s2out
                //s2in
                if (textBox7.Text == "0" || textBox7.Text == "8" || textBox7.Text == "16" || textBox7.Text == "24" || textBox7.Text == "32" || textBox7.Text == "40" || textBox7.Text == "48" || textBox7.Text == "56")
                {
                    textBox6.Text = "1";
                }
                else
                {
                    textBox6.Text = "0";


                }





            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            //at window start
            if (Convert.ToInt32(label1.Text) > Convert.ToInt32(label2.Text))
            {

                int xx = Convert.ToInt32(label2.Text);
                DateTime CurrTime = DateTime.Now;
                string yy = Convert.ToString(CurrTime - Convert.ToDateTime(dataGridView1.Rows[xx].Cells[2].Value));
                xx++;
                label44.Text = xx.ToString();
                string zz;
                zz= "'" + CurrTime.ToString() + "'";
                string xxx = xx.ToString();
                carcomm = new OleDbCommand("Update table1 set timeatwin= " + zz + "  where (car ='" + xxx + "')", carcon);
                carcomm.ExecuteNonQuery();

                //--------------------------
                carcomm = new OleDbCommand("Update table1 set timetoreachwin ='" + yy + "'  where (car ='" + xxx + "')", carcon);
                carcomm.ExecuteNonQuery();
                //--------------------------


                carcomm = new OleDbCommand("Select * from table1", carcon);
                caradapt = new OleDbDataAdapter();
                caradapt.SelectCommand = carcomm;
                cartable = new DataTable();
                caradapt.Fill(cartable);
                dataGridView1.DataSource = cartable;

            }
        
        //at window end
        
        }

        private void button8_Click(object sender, EventArgs e)
        {

            label45.Text = dataGridView1.Rows[1].Cells[0].Value.ToString();
        }


    }
}
