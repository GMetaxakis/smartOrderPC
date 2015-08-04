using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SmartOrder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// sto ksekinima tis efarmogis init tin basi kai ksekina ton http server gia aitimata apo to kinito
        /// </summary>
        public MainWindow()
        {
            DbCreator db = new DbCreator();
            db.init();
            InitializeComponent();

            startServer();
            (new Unlock()).ShowDialog();
        }

        MyHttpServer httpServer;
        /// <summary>
        /// sinartisi arxikopoiisis server kai event gia tin katastasi tou user kai ton paraggelion
        /// </summary>
        private void startServer()
        {
            httpServer = new MyHttpServer(8080);
            httpServer.setUserState += new MyHttpServer.setUserStateHandler(usersControl.setUserState);
            httpServer.updateOrders += new MyHttpServer.updateOrdersHandler(ordersControl.update);

            Thread serverThread = new Thread(new ThreadStart(httpServer.listen));
            serverThread.Start();
        }

        /// <summary>
        /// event poy kaleite ligo prin kleisei i efarmogi na stamatisei ton httpserver kai meta tin efarmogi
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            httpServer.Stop();
            Environment.Exit(0);
        }

        /// <summary>
        /// event apo to koumpi klisimatos
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window_Closing(this, null);
        }

        /// <summary>
        /// event apo to koumpi klidomatos
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (new Unlock()).ShowDialog();
        }
    }
}
