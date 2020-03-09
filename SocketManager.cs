using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace GameCaro
{
    class SocketManager
    {
        /*Nhiệm vụ của class này:
            +Tạo client và server
            + cung cấp 2 hàm  gửi và nhận tin
        */
        #region Both 
        // Dùng chung ip và port
        public string ip = Cons.IP;
        public int port = Cons.PORT;
        public bool isServer = true;

        public bool Send(object data)
        {
            byte[] sendData = SerializeData(data);
            return SentData(client, sendData); //thằng nào cũng dùng client để gửi, vì nếu là server thì gửi tiếp về server làm gì
            
        }

        public object Receive()
        {
            byte[] recieveData = new byte[Cons.BUFFER];
            bool isOK = ReceivedData(client, recieveData);

            return DeserializeData(recieveData);
        }
        private bool SentData(Socket target, byte[] data) // phải biết được socket nào gửi và socket nào nhận, và dĩ nhiên phải có data
        {
            return target.Send(data) == 1 ? true : false;
        }

        private bool ReceivedData(Socket target, byte[] data)
        {
            return target.Receive(data) == 1 ? true : false; 
        }
        /*  
            3 Hàm dùng chung luôn phải viết trong các class network
            + Khi truyền đi một dữ liệu, dữ liệu được biến đổi thành một mảng byte[]
            => Cần có hàm nén thành mảng byte và hàm giải nén nó để ctr có thể đọc đc
        */

        /// <sumary>
        /// Nén đối tượng thành mảng byte[]
        /// </sumary>
        /// <param name="o"></param>
        /// <returns></returns>
        ///
        public byte[] SerializeData(object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, o);
            return ms.ToArray();
        }

        ///<summary>
        ///Giải nén mảng byte[] thành đối tượng object
        /// </summary>
        /// <param name="theByteArray"></param>
        ///<returns></returns>
        ///
        public object DeserializeData(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;
            return bf1.Deserialize(ms);
        }

        ///<summary>
        /// Lấy ra ID V4 của card mạng đang dùng // dùng kết nối của Internet: cổng có dây hoặc Wifi: công không dây
        /// Người dùng không biết lấy IP
        /// Thay vì phải vào cmd gõ ipconfig rồi dò đến IPV4 thì hàm này giúp chúng ta thực hiện điều đó
        ///</summary>
        ///<param name="_type"></param>
        ///<returns></returns>
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }

        #endregion

        #region Server
        Socket server;
        public void CreatedServer()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //server.Connect(iep);
            server.Bind(iep); //đợi kết nối
            server.Listen(10); // đợi 60s

            // Tạo luồng kết nối riêng, giống kiểu đếm thời gian

            Thread acceptedClient = new Thread(() => // anonymous method// truyền paramater là phải object// hàm là hàm void
            {
                client = server.Accept();
                
            });
            // Tránh trường hợp ctr bị tắt ngang, thì Thread cũng tắt để đỡ tốn tài nguyên HĐH
            acceptedClient.IsBackground = true;
            acceptedClient.Start();
            ChessBoardManage.playerName.Text += " server";
        }
        #endregion

        #region Client
        Socket client;
        public bool ConnectedServer() // trả về bool vì chưa biết có kết nối được hay không
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);            
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(iep);
                return true;
            }
            catch
            {
                return false;
            }
            

            
        }

        #endregion
    }
}
