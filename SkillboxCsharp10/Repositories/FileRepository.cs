using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using SkillboxCsharp10.Clients;

namespace SkillboxCsharp10.Repositories
{
    /// <summary>
    /// Репозиторий по работе с текстовыми файлами.
    /// Реализует интерфейс IRepository.
    /// </summary>
    public class FileRepository : IRepository
    {
        /// <summary>
        /// Разделитель, используемый в файле.
        /// </summary>
        private const string ColumnSeparatorInFile = "#";

        /// <summary>
        /// Путь к файлу репозитория.
        /// </summary>
        private string clientsFilePath;

        /// <summary>
        /// Конструктор для создания репозитория.
        /// </summary>
        /// <param name="clientsFile">Путь к файлу.</param>
        public FileRepository(string clientsFile)
        {
            clientsFilePath = clientsFile;
        }

        #region Реализация интерфейса.

        public IEnumerable<Client> Load()
        {
            var clients = new List<Client>();

            using (StreamReader clientReader = new StreamReader(clientsFilePath))
            {
                string line;
                while ((line = clientReader.ReadLine()) != null)
                {
                    Client client = new Client(ClientInfoFromFileLine(line));

                    clients.Add(client);

                    ReadClientLogFromFile(client);
                }
            }

            return clients;
        }

        public void Save(IEnumerable<Client> clients)
        {
            using (StreamWriter clientWriter = new StreamWriter(clientsFilePath))
            {
                foreach (Client client in clients)
                {
                    string clientInfoLine = ClientToFileLine(client);

                    clientWriter.WriteLine(clientInfoLine);

                    WriteClientLogToFile(client);
                }
            }
        }

        public Client FindById(int id, IEnumerable<Client> clients)
        {
            foreach (var client in clients)
            {
                if (client.Id == id)
                {
                    return client;
                }
            }

            return null;
        }

        #endregion

        private void ReadClientLogFromFile(Client client)
        {
            string logFileName = GetClientLogFileName(client);

            if (File.Exists(logFileName))
            {
                using (StreamReader logReader = new StreamReader(GetClientLogFileName(client)))
                {
                    string logLine;
                    while ((logLine = logReader.ReadLine()) != null)
                    {
                        ChangeLogItem logItem = LogItemFromFileLine(logLine);
                        client.ChangeLog.Add(logItem);
                    }
                }
            }
        }

        private static ClientInfo ClientInfoFromFileLine(string line)
        {
            string[] lineParts = line.Split(ColumnSeparatorInFile);

            return new ClientInfo
            {
                Id = int.Parse(lineParts[0]),
                LastName = lineParts[1],
                FirstName = lineParts[2],
                Patronymic = lineParts[3],
                Passport = lineParts[4],
                Phone = lineParts[5],
            };
        }

        private static ChangeLogItem LogItemFromFileLine(string line)
        {
            string[] lineParts = line.Split(ColumnSeparatorInFile);

            return new ChangeLogItem(
                DateTime.ParseExact(lineParts[0], "s", CultureInfo.InvariantCulture),
                lineParts[2],
                lineParts[1]
                );
        }

        private static void WriteClientLogToFile(Client client)
        {
            using (StreamWriter logWriter = new StreamWriter(GetClientLogFileName(client)))
            {
                foreach (var logItem in client.ChangeLog)
                {
                    string logInfo = LogItemToFileLine(logItem);

                    logWriter.WriteLine(logInfo);
                }
            }
        }

        private static string GetClientLogFileName(Client client)
        {
            return $"{client.Id}.txt";
        }

        private static string ClientToFileLine(Client client)
        {
            return string.Join(ColumnSeparatorInFile,
                client.Id,
                client.LastName,
                client.FirstName,
                client.Patronymic,
                client.Phone,
                client.Passport
                );
        }

        private static string LogItemToFileLine(ChangeLogItem logItem)
        {
            return string.Join(ColumnSeparatorInFile,
                logItem.Date.ToString("s"),
                logItem.Employee,
                logItem.ChangeInfo);
        }

        public int GetNextId()
        {
            var lastLine = File.ReadLines(clientsFilePath).LastOrDefault();

            if (lastLine == null)
            {
                return 1;
            }

            var lastClient = ClientInfoFromFileLine(lastLine);

            return lastClient.Id + 1;
        }
    }
}
