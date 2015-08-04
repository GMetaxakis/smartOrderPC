using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SmartOrder
{
    /// <summary>
    /// i klassi pou xrisimopoieitai gia tin ektiposi ton apodikseon
    /// </summary>
    class Printer
    {
        public static void printTest()
        {
            PrintDialog printDlg = new PrintDialog();

            String textToPrint = "";
            textToPrint += "\t\t Τραπέζι 1\n\n";
            
            textToPrint += "Παραγγελίες : \n";

            textToPrint += "\t1 freddo espresso σκέτο \n";
            textToPrint += "\t1 freddo espresso μέτριο \n";
            textToPrint += "\t1 freddo espresso σκέτο \n";

            textToPrint += "\n\n";

            textToPrint += "Σερβιτόρος : waiter\n";
            textToPrint += "Ώρα : " + DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt");


            FlowDocument doc = new FlowDocument(new Paragraph(new Run(textToPrint)));
            doc.Name = "FlowDoc";
            IDocumentPaginatorSource idpSource = doc;
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
        }

        public static void printOrder( Order o )
        {
            PrintDialog printDlg = new PrintDialog();

            String textToPrint = "";
            textToPrint += "\t\t Τραπέζι "+o.Table_id+"\n\n";

            textToPrint += "Παραγγελία : \n";
            String orderName = "";
            foreach (Items i in DBController.LoadItems())
            {
                if (i.Id == o.Items_id)
                    orderName = i.Name;
            }

            orderName += o.CharacteristicsInfo;
            textToPrint += "\t1 "+orderName+"\n";

            textToPrint += "\n\n";

            String waiterName = "";
            foreach (User u in DBController.LoadUsers())
            {
                if (u.User_id == o.User_id)
                    waiterName = u.Name;
            }

            textToPrint += "Σερβιτόρος : "+waiterName+"\n";
            textToPrint += "Ώρα : " + DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt");

            FlowDocument doc = new FlowDocument(new Paragraph(new Run(textToPrint)));
            doc.Name = "FlowDoc";
            IDocumentPaginatorSource idpSource = doc;
            printDlg.PrintDocument(idpSource.DocumentPaginator, "smart order");
        }
    
        public static void printOrders( List<Order> orders)
        {

            PrintDialog printDlg = new PrintDialog();

            String textToPrint = "";
            textToPrint += "\t\t Τραπέζι " + orders[0].Table_id + "\n\n";

            textToPrint += "Παραγγελίες : \n";

            foreach( Order o in orders)
            {
                String orderName = "";
                foreach (Items i in DBController.LoadItems())
                {
                    if (i.Id == o.Items_id)
                        orderName = i.Name;
                }

                orderName += o.CharacteristicsInfo;
                textToPrint += "\t1 " + orderName + "\n";
            }
            textToPrint += "\n\n";

            String waiterName = "";
            foreach (User u in DBController.LoadUsers())
            {
                if (u.User_id == orders[0].User_id)
                    waiterName = u.Name;
            }

            textToPrint += "Σερβιτόρος : " + waiterName + "\n";
            textToPrint += "Ώρα : " + DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt");

            FlowDocument doc = new FlowDocument(new Paragraph(new Run(textToPrint)));
            doc.Name = "FlowDoc";
            IDocumentPaginatorSource idpSource = doc;
            printDlg.PrintDocument(idpSource.DocumentPaginator, "smart order");

        }
    }
}
