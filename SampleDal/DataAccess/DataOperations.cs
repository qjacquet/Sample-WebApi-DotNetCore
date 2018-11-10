namespace Sample.Dal.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using DalModels;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    public class DataOperations : IDataOperations
    {
        private readonly string _filePath;
        private readonly string _filePrefix;

        public DataOperations(IConfiguration configuration)
        {
            var configurationService = configuration;
            _filePath = configurationService["filepath"];
            _filePrefix = configurationService["customerFilePrefix"];
        }

        public bool CreateOrUpdateCustomerRecord(Customer customer)
        {
            customer.Id = FindNextCustomerNumber();

            try
            {
                using (var file = File.CreateText(CustomerFileNameAndPath(customer.Id)))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, customer);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Customer FetchExistingCustomer(int customerId)
        {
            Customer customer;

            var customerFilename = CustomerFileNameAndPath(customerId);

            if (!File.Exists(customerFilename))
            {
                return null;
            }

            using (var streamReader = new StreamReader(customerFilename))
            {
                var jsonString = streamReader.ReadToEnd();
                customer = JsonConvert.DeserializeObject<Customer>(jsonString);
            }

            return customer;
        }

        public IEnumerable<Customer> FetchAllCustomers()
        {
            var files = FetchAllCustomerFiles();
            return files.Select(file => FetchExistingCustomer(CustomerNumberFromFilename(file))).ToList();
        }


        private string CustomerFileNameAndPath(int customerNumber)
        {
            return $"{_filePath}{_filePrefix}{customerNumber.ToString()}.txt";
        }
        private int CustomerNumberFromFilename(FileInfo file)
        {
            var lengthToTrim = file.Name.Length - (_filePrefix.Length);
            var fileNameFrontTrimmed = file.Name.Substring(_filePrefix.Length, lengthToTrim);
            var fileNameNumber = fileNameFrontTrimmed.Substring(0, fileNameFrontTrimmed.Length - 4);

            return int.TryParse(fileNameNumber, out var fileNumber) ? fileNumber : 0;
        }

        private IEnumerable<FileInfo> FetchAllCustomerFiles()
        {
            var dirInfo = GetDirectory();
            return dirInfo.GetFiles($"{_filePrefix}*.txt");
        }

        private DirectoryInfo GetDirectory()
        {
            return Directory.CreateDirectory(_filePath);
        }

        private int FindNextCustomerNumber()
        {
            var files = FetchAllCustomerFiles();

            var highestFileNumber = 0;
            foreach (var file in files)
            {
                var customerId = CustomerNumberFromFilename(file);
                if (customerId == 0) continue;

                if (customerId > highestFileNumber)
                {
                    highestFileNumber = customerId;
                }
            }

            return highestFileNumber + 1;
        }
    }
}
