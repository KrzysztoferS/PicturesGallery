using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobSotrage
{
    class BlobController
    {
        public bool SavePicturBlob(IFormFile picture, Guid userGuid)
        {
            //TODO
            //zapisuje zdjecie w contenerze ktore jest guidem uzytkownika
            return false;
        }

        public bool CreateContainer(Guid userGuid)
        {
            //TODO
            //Jesli nie ma kontenera to go tworzy, jesli jest to nic nie robi
            return false;
        }

        public bool DeletePicture(string pictureName, Guid userGuid)
        {
            //usuwa plik o podanej nazwie z kontenera userGuid
            return false;
        }
    }
}
