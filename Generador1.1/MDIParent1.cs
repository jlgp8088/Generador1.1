using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generador1._1
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            Form1 configuracion = new Form1();
            configuracion.MdiParent = this;
            configuracion.Show();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            DataSet1 dss;
            int lineaAct = 0;
            int countCompany = 0;
            int countClient = 0;
            int countSeller = 0;
            int countSeparator = 1;
            int countItem = 0;
            int countDocument = 0;
            int countDescripcion = 0;
            int count = 0;
            int countfacturas = 0;
            int countCopy = 0;
            float pesototal = 0;
            string typePay = "";
            string caducityDate = "";
            string firstCaracter = "";
            string horaentrega = "";
            int countfile = 0;
            int[] pointSeparator = new int[11] { 1, 11, 42, 54, 62, 68, 82, 93, 99, 118, 136 };
            string copia = "";
            string[] dataCompany = new string[5];
            string[] dataDocument = new string[23];
            string[] dataClient = new string[7];
            string[] dataSeller = new string[2];
            string[,] items = new string[10, 30];


            if (Properties.Settings.Default.ruta.Trim() != "")
            {
                StreamReader objReader1 = new StreamReader(Properties.Settings.Default.ruta);
                string sLine1 = "";
                while (sLine1 != null)
                {
                    if (sLine1.Trim() != "")
                    {
                        if (sLine1.Substring(0, 1) == "+")
                        {
                            countfacturas += 1;
                        }
                    }
                    sLine1 = objReader1.ReadLine();
                }
                countfacturas = countfacturas / 7 / 2;
                objReader1.Close();

            }


            // MessageBox.Show(countfacturas.ToString());

            //textBox2.Text = "";
            if (Properties.Settings.Default.ruta.Trim() != "")
            {
                DataSet1 ds = new DataSet1();

                StreamReader objReader = new StreamReader(Properties.Settings.Default.ruta);
                string sLine = "";
                while (sLine != null)
                {

                    if (sLine.Trim() != "" && sLine != null)
                    {
                        firstCaracter = sLine.Substring(0, 1);
                        if (firstCaracter.ToString() == "+")
                        {
                            countSeparator += 1;
                        }
                        else
                        {
                            if (countSeparator == 1)
                            {
                                string validcaracter = sLine.Substring(0, 2);
                                if (validcaracter != "|-")
                                {

                                    if (sLine.Substring(0, 30).IndexOf(":", 0) != -1)
                                    {
                                        dataCompany[countCompany] = sLine.Substring(sLine.IndexOf(":", 0) + 1, 60).Trim();
                                    }
                                    else
                                    {
                                        dataCompany[countCompany] = sLine.Substring(1, 87).Trim();
                                    }

                                    if (sLine.IndexOf(":", 87) != -1)
                                    {
                                        dataDocument[countDocument] = sLine.Substring(sLine.IndexOf(":", 87) + 1, 15).Trim();
                                        countDocument += 1;
                                        //MessageBox.Show(dataDocument[countCompany].ToString());
                                    }
                                    countCompany += 1;
                                }

                            }

                            if (countSeparator == 2)
                            {
                                countfile += 1;
                                if (countfile == 2)
                                {
                                    typePay = sLine.Substring(sLine.IndexOf("|", 86) + 1, sLine.IndexOf("|", 89) - sLine.IndexOf("|", 86) - 1).Trim();
                                    //MessageBox.Show(typePay.ToString());
                                    caducityDate = sLine.Substring(sLine.IndexOf("|", 100) + 1, sLine.IndexOf("|", 119) - sLine.IndexOf("|", 100) - 1).Trim();
                                    //MessageBox.Show(caducityDate.ToString());

                                }
                                if (sLine.IndexOf(":", 1) != -1)
                                {
                                    dataClient[countClient] = sLine.Substring(sLine.IndexOf(":", 1) + 1, 50 - sLine.IndexOf(":", 1)).Trim();
                                    //MessageBox.Show(dataClient[countClient].ToString());
                                    if (countfile == 2)
                                    {
                                        horaentrega = sLine.Substring(60, 28).Trim();
                                    }
                                }
                                countClient += 1;
                                if (countfile == 4 || countfile == 5)
                                {
                                    dataClient[countClient] = sLine.Substring(sLine.IndexOf(":", 50) + 1, 87 - sLine.IndexOf(":", 50)).Trim();
                                    countClient += 1;
                                    dataSeller[countSeller] = sLine.Substring(sLine.IndexOf(":", 86) + 1, sLine.IndexOf("|", 89) - sLine.IndexOf(":", 86) - 1).Trim();
                                    countSeller += 1;
                                }

                            }

                            if (countSeparator == 4)
                            {
                                string firstItem = sLine.Substring(1, 9);
                                if (firstItem.Trim().Length != 0)
                                {
                                    while (countDescripcion <= 9)
                                    {

                                        items[countDescripcion, countItem] = sLine.Substring(pointSeparator[countDescripcion], pointSeparator[countDescripcion + 1] - pointSeparator[countDescripcion]).Trim();
                                        countDescripcion += 1;

                                    }
                                    countDescripcion = 0;
                                    countItem += 1;

                                }

                            }

                            if (countSeparator == 5)
                            {

                                int initialPoint = 1;
                                int finalPoint = 0;

                                while (finalPoint < 136)
                                {
                                    finalPoint = sLine.IndexOf("|", initialPoint + 1);

                                    dataDocument[countDocument] = sLine.Substring(initialPoint + 1, finalPoint - initialPoint - 1);

                                    initialPoint = finalPoint;
                                    //textBox2.Text = textBox2.Text + "/" + dataDocument[countDocument].ToString();
                                    countDocument += 1;
                                }

                            }

                            if (countSeparator == 6)
                            {
                                dataDocument[countDocument] = sLine.Substring(sLine.IndexOf(":", 2) + 1, 90).Trim();
                                countDocument += 1;
                            }

                            if (countSeparator == 7)
                            {
                                count += 1;
                                dataDocument[countDocument] = sLine.Substring(1, 134).Trim();
                                countDocument += 1;

                            }
                            if (countSeparator == 8)
                            {
                                if (countCopy >= countfacturas)
                                {
                                    copia = "1";
                                }
                                else
                                {
                                    copia = "0";
                                }
                                int countf = 0;
                                while (countf < countItem)
                                {
                                    DataRow row = ds.DataTable1.NewDataTable1Row();
                                    countDescripcion = 0;
                                    while (countDescripcion <= 9)
                                    {
                                        string campo = "";

                                        if (countDescripcion == 0) { campo = "item"; }
                                        if (countDescripcion == 1) { campo = "descripcion"; }
                                        if (countDescripcion == 2) { campo = "peso"; }
                                        if (countDescripcion == 3) { campo = "cant"; }
                                        if (countDescripcion == 4) { campo = "um"; }
                                        if (countDescripcion == 5) { campo = "preciounit"; }
                                        if (countDescripcion == 6) { campo = "descuento"; }
                                        if (countDescripcion == 7) { campo = "iva"; }
                                        if (countDescripcion == 8) { campo = "precionetounit"; }
                                        if (countDescripcion == 9) { campo = "valortotal"; }
                                        if (countDescripcion == 2)

                                        {
                                            row[campo] = Convert.ToSingle(items[2, countf].Replace(".", ",")) * Convert.ToSingle(items[3, countf].Replace(".", ","));
                                            pesototal = pesototal + Convert.ToSingle(items[2, countf].Replace(".", ",")) * Convert.ToSingle(items[3, countf].Replace(".", ","));
                                        }
                                        else
                                        {
                                            row[campo] = items[countDescripcion, countf].Trim();
                                        }
                                        countDescripcion += 1;
                                    }
                                    row["nombreempresa"] = Properties.Settings.Default.companyname.Trim();
                                    row["nitempresa"] = dataCompany[1].Trim();
                                    row["dirempresa"] = dataCompany[2].Trim();
                                    row["telempresa"] = dataCompany[3].Trim();
                                    row["numfactura"] = dataDocument[0].Trim();
                                    row["fechaFactura"] = dataDocument[1].Trim();
                                    row["nomcliente"] = dataClient[0].Trim();
                                    row["nomestablecimiento"] = dataClient[1].Trim();
                                    row["nitcliente"] = dataClient[2].Trim();
                                    row["dircliente"] = dataClient[3].Trim();
                                    row["ciucliente"] = dataClient[5].Trim();
                                    row["barriocliente"] = dataClient[4].Trim();
                                    row["telcliente"] = dataClient[6].Trim();
                                    row["formapago"] = typePay.Trim();
                                    row["fechavencimiento"] = caducityDate.Trim();
                                    row["nomvendedor"] = dataSeller[0].Trim();
                                    row["codvendedor"] = dataSeller[1].Trim();
                                    row["totalbruto"] = dataDocument[11].Trim();
                                    row["totaldescuento"] = dataDocument[12].Trim();
                                    row["subtotal"] = dataDocument[13].Trim();
                                    row["totaliva"] = dataDocument[14].Trim();
                                    row["totalfactura"] = dataDocument[15].Trim();
                                    row["retefte"] = dataDocument[16].Trim();
                                    row["reteica"] = dataDocument[17].Trim();
                                    row["reteiva"] = dataDocument[18].Trim();
                                    row["netopagar"] = dataDocument[19].Trim();
                                    row["valorletra"] = dataDocument[20].Trim();
                                    row["resolucion"] = "value_par1";
                                    row["legalheader"] = Properties.Settings.Default.header.Trim();
                                    row["legalfooter"] = Properties.Settings.Default.footer.Trim() + dataDocument[22].Substring(1, 94);
                                    row["pesototal"] = Math.Round(pesototal, 2);
                                    row["copia"] = copia;
                                    row["horaentrega"] = horaentrega.Trim();
                                    row["codebar"] = "*" + dataDocument[0].Trim() + "*";
                                    ds.DataTable1.Rows.Add(row);
                                    countf += 1;
                                }
                                countCopy += 1;
                                countfile = 0;
                                countCompany = 0;
                                countClient = 0;
                                countSeller = 0;
                                countSeparator = 1;
                                countItem = 0;
                                countDocument = 0;
                                countDescripcion = 0;
                                count = 0;

                                pesototal = 0;
                                typePay = "";
                                caducityDate = "";

                            }

                        }
                    }


                    sLine = objReader.ReadLine();
                    lineaAct += 1;
                }
                int count1 = 0;
                while (count1 < countItem)
                {
                    DataRow rowf = ds.DataTable1.NewDataTable1Row();
                    countDescripcion = 0;
                    while (countDescripcion <= 9)
                    {
                        string campo = "";
                        if (countDescripcion == 0) { campo = "item"; }
                        if (countDescripcion == 1) { campo = "descripcion"; }
                        if (countDescripcion == 2) { campo = "peso"; }
                        if (countDescripcion == 3) { campo = "cant"; }
                        if (countDescripcion == 4) { campo = "um"; }
                        if (countDescripcion == 5) { campo = "preciounit"; }
                        if (countDescripcion == 6) { campo = "descuento"; }
                        if (countDescripcion == 7) { campo = "iva"; }
                        if (countDescripcion == 8) { campo = "precionetounit"; }
                        if (countDescripcion == 9) { campo = "valortotal"; }
                        if (countDescripcion == 2)
                        {
                            rowf[campo] = Convert.ToSingle(items[2, count1].Replace(".", ",")) * Convert.ToSingle(items[3, count1].Replace(".", ","));
                            pesototal = pesototal + Convert.ToSingle(items[2, count1].Replace(".", ",")) * Convert.ToSingle(items[3, count1].Replace(".", ","));
                        }
                        else
                        {
                            rowf[campo] = items[countDescripcion, count1].Trim();
                        }
                        countDescripcion += 1;
                    }
                    rowf["nombreempresa"] = Properties.Settings.Default.companyname.Trim();
                    rowf["nitempresa"] = dataCompany[1].Trim();
                    rowf["dirempresa"] = dataCompany[2].Trim();
                    rowf["telempresa"] = dataCompany[3].Trim();
                    rowf["numfactura"] = dataDocument[0].Trim();
                    rowf["fechaFactura"] = dataDocument[1].Trim();
                    rowf["nomcliente"] = dataClient[0].Trim();
                    rowf["nomestablecimiento"] = dataClient[1].Trim();
                    rowf["nitcliente"] = dataClient[2].Trim();
                    rowf["dircliente"] = dataClient[3].Trim();
                    rowf["ciucliente"] = dataClient[5].Trim();
                    rowf["barriocliente"] = dataClient[4].Trim();
                    rowf["telcliente"] = dataClient[6].Trim();
                    rowf["formapago"] = typePay.Trim();
                    rowf["fechavencimiento"] = caducityDate.Trim();
                    rowf["nomvendedor"] = dataSeller[0].Trim();
                    rowf["codvendedor"] = dataSeller[1].Trim();
                    rowf["totalbruto"] = dataDocument[11].Trim();
                    rowf["totaldescuento"] = dataDocument[12].Trim();
                    rowf["subtotal"] = dataDocument[13].Trim();
                    rowf["totaliva"] = dataDocument[14].Trim();
                    rowf["totalfactura"] = dataDocument[15].Trim();
                    rowf["retefte"] = dataDocument[16].Trim();
                    rowf["reteica"] = dataDocument[17].Trim();
                    rowf["reteiva"] = dataDocument[18].Trim();
                    rowf["netopagar"] = dataDocument[19].Trim();
                    rowf["valorletra"] = dataDocument[20].Trim();
                    rowf["resolucion"] = "value_par1";
                    rowf["legalheader"] = Properties.Settings.Default.header.Trim();
                    rowf["legalfooter"] = Properties.Settings.Default.footer.Trim();
                    rowf["pesototal"] = Math.Round(pesototal, 2);
                    rowf["copia"] = copia.Trim();
                    rowf["horaentrega"] = horaentrega.Trim();
                    rowf["codebar"] = "*" + dataDocument[0].Trim() + "*";
                    ds.DataTable1.Rows.Add(rowf);
                    count1 += 1;
                }

                objReader.Close();

                dss = new DataSet1();
                dss.Merge(ds);
                DataTable1BindingSource.DataSource = dss;
                configuracion.Enabled = false;
                toolStripButton1.Enabled = true;

                ReportParameter[] parameters = new ReportParameter[1];

                parameters[0] = new ReportParameter("observation", Properties.Settings.Default.observation.ToString().Trim());


                this.reportViewer1.LocalReport.SetParameters(parameters);
                this.reportViewer1.RefreshReport();

                reportViewer1.Visible = true;

            }
            else
            {
                MessageBox.Show("Debe Seleccionar Algun Archivo Desde Configuracion");
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            configuracion.Enabled = true;
            toolStripButton1.Enabled = false;
            reportViewer1.Visible = false;
        }
    }
}
