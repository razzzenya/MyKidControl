using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;


namespace MainLibrary
{

    public class Logger
    {
        public void ClearLogs()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string logFilePath = Path.Combine(directoryPath, "logs.dat");
            File.WriteAllText(logFilePath, "");
        }

        public void LogStartApp()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Программа была запущена\n");
        }

        public void LogExitApp()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Программа была закрыта\n");
        }

        public void LogThatUserNotified(string username)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Время пользователя {username} закончилось и он был уведомлён\n");
        }

        public void LogStartSession(string username)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Была начата сессия в профиле {username}\n");
        }

        public void LogEndSession(string username)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Была закончена сессия в профиле {username}\n");
        }

        public void LogEndSessionWhenTimeIsGone(string username)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Была закончена сессия в профиле {username} по нажатию кнопки \"Завершить\" после окончания допустимого времени у всех процессов\n");
        }

        public void LogForciblyEndSession(string username)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Была принудтельно закончена сессия в профиле {username}\n");
        }

        public void LogNewSuperuserPassword(string old_password, string password)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Пароль суперпользователя был изменен с {old_password} на {password}\n");
        }

        public void LogLogIn(string username)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Был произведен вход в профиль {username}\n");
        }

        public void LogSuserLogIn()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Был произведен вход как суперпользователь\n");
        }

        public void LogSuserLogOut()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Был произведен выход из прав суперпользователя\n");
        }

        public void LogUserProcessesChanged(string username)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Был отредактирован список процессов у пользователя {username}\n");
        }

        public void LogUsernameChanged(string old_username, string username)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Имя пользователя было изменено с {old_username} на {username}\n");
        }

        public void LogUserPasswordChanged(string username, string old_password, string password)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Пароль пользователя {username} был изменен с {old_password} на {password}\n");
        }

        public void LogCreatingUser(string username)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Был создан профиль {username}\n");
        }

        public void LogDeletingUser(string username)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Был удалён профиль {username}\n");
        }

        public void LogDeletedSuserFile()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (!File.Exists(flagFilePath))
            {
                LogStart();
            }
            SaveLogToFile($"{DateTime.Now} : Был удалён файл суперпользователя\n");
        }

        public void LogStart()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            Directory.CreateDirectory(directoryPath);
            File.WriteAllText(flagFilePath, "");
            SaveLogToFile($"{DateTime.Now} : Старт работы\n");
        }

        public void SaveLogToFile(string logMessage)
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string logFilePath = Path.Combine(directoryPath, "logs.dat");

            using (FileStream fs = new FileStream(logFilePath, FileMode.Append))
            {
                IFormatter formatter = new BinaryFormatter();
                byte[] encryptedData = EncryptLog(logMessage);
                formatter.Serialize(fs, encryptedData);
            }
        }

        private byte[] EncryptLog(string logMessage)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, logMessage);

                char[] key = { 'V', 'h', 'a', 'E', '3', 'O', 'I', 'L', 'q', 'c', 'O', 'E', 'm', 'H', '1', 'l' };
                byte[] plaintextBytes = memoryStream.ToArray();
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                IBufferedCipher cipher = CipherUtilities.GetCipher("AES/ECB/PKCS7Padding");
                cipher.Init(true, new KeyParameter(keyBytes));

                return cipher.DoFinal(plaintextBytes);
            }
        }

        public List<string> LoadLogsFromFile()
        {
            List<string> logs = new List<string>();

            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string logFilePath = Path.Combine(directoryPath, "logs.dat");

            if (File.Exists(logFilePath))
            {
                using (FileStream fs = new FileStream(logFilePath, FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();
                    while (fs.Position < fs.Length)
                    {
                        byte[] encryptedData = (byte[])formatter.Deserialize(fs);
                        logs.Add(DecryptBytesToLog(encryptedData));
                    }
                }
            }

            return logs;
        }

        private string DecryptBytesToLog(byte[] encryptedData)
        {
            using (MemoryStream memoryStream = new MemoryStream(encryptedData))
            {
                char[] key = { 'V', 'h', 'a', 'E', '3', 'O', 'I', 'L', 'q', 'c', 'O', 'E', 'm', 'H', '1', 'l' };
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                IBufferedCipher cipher = CipherUtilities.GetCipher("AES/ECB/PKCS7Padding");
                cipher.Init(false, new KeyParameter(keyBytes));

                using (MemoryStream decryptedStream = new MemoryStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = memoryStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        byte[] output = cipher.ProcessBytes(buffer, 0, bytesRead);
                        decryptedStream.Write(output, 0, output.Length);
                    }

                    byte[] finalOutput = cipher.DoFinal();
                    decryptedStream.Write(finalOutput, 0, finalOutput.Length);

                    decryptedStream.Position = 0;
                    IFormatter formatter = new BinaryFormatter();
                    return (string)formatter.Deserialize(decryptedStream);
                }
            }
        }
    }
    public class SuperUser
    {
        private string _superuser_password;

        public string SuperuserPassword
        {
            get => _superuser_password;
            set { _superuser_password = value; }
        }

        public void SaveToFile()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string filePath = Path.Combine(directoryPath, "suser.dat");
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                IFormatter formatter = new BinaryFormatter();
                byte[] encryptedData = EncryptObject();
                formatter.Serialize(fs, encryptedData);
            }
        }

        private byte[] EncryptObject()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, SuperuserPassword);

                char[] key = { 'V', 'h', 'a', 'E', '3', 'O', 'I', 'L', 'q', 'c', 'O', 'E', 'm', 'H', '1', 'l' };
                byte[] plaintextBytes = memoryStream.ToArray();
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                IBufferedCipher cipher = CipherUtilities.GetCipher("AES/ECB/PKCS7Padding");
                cipher.Init(true, new KeyParameter(keyBytes));

                return cipher.DoFinal(plaintextBytes);
            }
        }

        public bool LoadFromFile()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string filePath = Path.Combine(directoryPath, "suser.dat");

            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();
                    byte[] encryptedData = (byte[])formatter.Deserialize(fs);

                    SuperuserPassword = DecryptBytesToObject(encryptedData);
                }
                return true;
            }
            return false;
        }

        private string DecryptBytesToObject(byte[] encryptedData)
        {
            using (MemoryStream memoryStream = new MemoryStream(encryptedData))
            {
                char[] key = { 'V', 'h', 'a', 'E', '3', 'O', 'I', 'L', 'q', 'c', 'O', 'E', 'm', 'H', '1', 'l' };
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                IBufferedCipher cipher = CipherUtilities.GetCipher("AES/ECB/PKCS7Padding");
                cipher.Init(false, new KeyParameter(keyBytes));

                using (MemoryStream decryptedStream = new MemoryStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = memoryStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        byte[] output = cipher.ProcessBytes(buffer, 0, bytesRead);
                        decryptedStream.Write(output, 0, output.Length);
                    }

                    byte[] finalOutput = cipher.DoFinal();
                    decryptedStream.Write(finalOutput, 0, finalOutput.Length);

                    decryptedStream.Position = 0;
                    IFormatter formatter = new BinaryFormatter();
                    return (string)formatter.Deserialize(decryptedStream);
                }
            }
        }
    }

    [Serializable]
    public class User
    {
        private Dictionary<string, string> _processes;

        public Dictionary<string, string> Processes
        {
            get { return _processes ?? (_processes = new Dictionary<string, string>()); }
            set { _processes = value; }
        }

        private Dictionary<DateTime, TimeSpan> _timestatistics;

        public Dictionary<DateTime, TimeSpan> TimeStatistics
        {
            get { return _timestatistics ?? (_timestatistics = new Dictionary<DateTime, TimeSpan>()); }
            set { _timestatistics = value; }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set { _name = value; }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set { _password = value; }
        }

        public User(){}

        ~User()
        {
            _processes?.Clear();
            _processes = null;
        }
    }

    public class UsersCollection
    {
        private List<User> _users;
        public UsersCollection()
        {
            _users = new List<User>();
        }
        public List<User> Users
        {
            get { return _users ?? (_users = new List<User>()); }
            set { _users = value; }
        }

        public void AddUser(User user) => _users.Add(user);

        public void SaveToFile()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string key = "Br77xZqVPPKct0KH";
            string filePath = Path.Combine(directoryPath, "users.dat");
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                IFormatter formatter = new BinaryFormatter();
                byte[] encryptedData = EncryptObject(_users, key);
                formatter.Serialize(fs, encryptedData);
            }
        }

        private byte[] EncryptObject(object obj, string key)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, obj);

                byte[] plaintextBytes = memoryStream.ToArray();
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                IBufferedCipher cipher = CipherUtilities.GetCipher("AES/ECB/PKCS7Padding");
                cipher.Init(true, new KeyParameter(keyBytes));

                return cipher.DoFinal(plaintextBytes);
            }
        }

        public bool LoadFromFile()
        {
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string filePath = Path.Combine(directoryPath, "users.dat");
            string key = "Br77xZqVPPKct0KH";
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();
                    byte[] encryptedData = (byte[])formatter.Deserialize(fs);

                    _users = DecryptBytesToObject<UsersCollection>(encryptedData, key);
                }
                return true;
            }
            return false;
        }

        private List<User> DecryptBytesToObject<T>(byte[] encryptedData, string key)
        {
            using (MemoryStream memoryStream = new MemoryStream(encryptedData))
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                IBufferedCipher cipher = CipherUtilities.GetCipher("AES/ECB/PKCS7Padding");
                cipher.Init(false, new KeyParameter(keyBytes));

                using (MemoryStream decryptedStream = new MemoryStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = memoryStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        byte[] output = cipher.ProcessBytes(buffer, 0, bytesRead);
                        decryptedStream.Write(output, 0, output.Length);
                    }

                    byte[] finalOutput = cipher.DoFinal();
                    decryptedStream.Write(finalOutput, 0, finalOutput.Length);

                    decryptedStream.Position = 0;
                    IFormatter formatter = new BinaryFormatter();
                    return (List<User>)formatter.Deserialize(decryptedStream);
                }
            }
        }

        ~UsersCollection()
        {
            _users?.Clear();
            _users = null;
        }

    }

}