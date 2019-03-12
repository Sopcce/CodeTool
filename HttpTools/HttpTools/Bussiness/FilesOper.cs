using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using RandomCheck.Bussiness;

namespace RandomCheck.Bussiness
{
    class FilesOper
    {
      
        public bool Copy(string oldpath, string newpath)
        {
            //ʹ�û��������ٸ����ļ�
            string MySourceFile = oldpath;
            string MyTargetFile = newpath;
            Stream MyInputStream, MyOutputStream;
            BufferedStream MyBufferedInput, MyBufferedOutput;
            byte[] MyBuffer=new byte[1024] ;
            int  MyBytesRead;            
            try
            {
               //������������
               MyInputStream = File.OpenRead(MySourceFile);
               MyOutputStream = File.OpenWrite(MyTargetFile);
               //���������ƻ�����
               MyBufferedInput = new BufferedStream(MyInputStream);
               MyBufferedOutput = new BufferedStream(MyOutputStream);
               MyBytesRead = MyBufferedInput.Read(MyBuffer, 0, 1024);
               while (MyBytesRead > 0)
               {
                 MyBufferedOutput.Write(MyBuffer, 0, MyBytesRead);
                 MyBytesRead = MyBufferedInput.Read(MyBuffer, 0, 1024);
               }
               MyBufferedOutput.Flush();
               MyBufferedInput.Close();
               MyBufferedOutput.Close();


               return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Save(string path, MemoryStream memStream)
        {
             //ʹ�û��������ٸ����ļ�

            string MyTargetFile = path;
            memStream.Position = 0;
            Stream MyOutputStream;
            BufferedStream MyBufferedInput, MyBufferedOutput;
            byte[] MyBuffer=new byte[10] ;
            int  MyBytesRead;            
            try
            {
               //������������              
               MyOutputStream = File.OpenWrite(MyTargetFile);
               //���������ƻ�����
               MyBufferedInput = new BufferedStream(memStream);
               MyBufferedOutput = new BufferedStream(MyOutputStream);
               MyBytesRead = MyBufferedInput.Read(MyBuffer, 0, 10);
               while (MyBytesRead > 0)
               {
                 MyBufferedOutput.Write(MyBuffer, 0, MyBytesRead);
                 MyBytesRead = MyBufferedInput.Read(MyBuffer, 0, 10);
               }
               MyBufferedOutput.Flush();
               memStream.Close();
               MyBufferedOutput.Close();


               return true;
            }
            catch
            {
                return false;
            }
        }

        public object Get(string path)
        {
            //�������л����һ������
            ClassSerializers cs = new ClassSerializers();            
            
            //huode
            string MySourceFile = path;
            
            
            Stream MyInputStream;
            BufferedStream MyBufferedInput;
            MemoryStream MymemStream;
            byte[] MyBuffer = new byte[10];
            int MyBytesRead;
            try
            {
                //������������
                MyInputStream = File.OpenRead(MySourceFile);

                MymemStream = new MemoryStream();
                //���������ƻ�����
                MyBufferedInput = new BufferedStream(MyInputStream);
                
                
                MyBytesRead = MyBufferedInput.Read(MyBuffer, 0, 10);
                while (MyBytesRead > 0)
                {
                    MymemStream.Write(MyBuffer, 0, MyBytesRead);
                    MyBytesRead = MyBufferedInput.Read(MyBuffer, 0, 10);
                }
                MyBufferedInput.Close();
                

                object obj = cs.DeSerializeBinary(MymemStream);

                MymemStream.Close();
               

                return obj;
            }
            catch 
            {
                return null;
            }
        }
    }

}
