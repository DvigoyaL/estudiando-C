using System.Data.SQLite;
public class ArchivoDAO
{
    private readonly SQLiteConnection _connection;

    public ArchivoDAO(SQLiteConnection connection)
    {
        _connection = connection;
    }

    public void GuardarArchivo(string filePath)
    {
        byte[] fileBytes = File.ReadAllBytes(filePath); // Leer el archivo como bytes
        string fileName = Path.GetFileName(filePath);
        string fileType = ObtenerTipoMime(filePath); // Obtener el tipo MIME del archivo

        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"
                INSERT INTO Archivos (NombreArchivo, TipoArchivo, Archivo)
                VALUES ($nombreArchivo, $tipoArchivo, $archivo)";

            command.Parameters.AddWithValue("$nombreArchivo", fileName);
            command.Parameters.AddWithValue("$tipoArchivo", fileType);
            command.Parameters.AddWithValue("$archivo", fileBytes);

            command.ExecuteNonQuery();
        }
    }

    public string ObtenerTipoMime(string archivoPath)
    {
        var extension = Path.GetExtension(archivoPath).ToLowerInvariant();

        return extension switch
        {
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".pdf" => "application/pdf",
            _ => "application/octet-stream" // Tipo MIME por defecto
        };
    }

    public void LeerArchivo(int archivoId, string savePath)
    {
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT NombreArchivo, Archivo FROM Archivos WHERE Id = $id";
            command.Parameters.AddWithValue("$id", archivoId);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    string fileName = reader.GetString(0);
                    byte[] fileBytes = (byte[])reader["Archivo"];
                    File.WriteAllBytes(Path.Combine(savePath, fileName), fileBytes); // Guardar el archivo en el sistema de archivos
                }
            }
        }
    }

    public void EliminarArchivo(int archivoId)
    {
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM Archivos WHERE Id = $id";
            command.Parameters.AddWithValue("$id", archivoId);
            command.ExecuteNonQuery();
        }
    }

    public IEnumerable<(int Id, string NombreArchivo)> ListarArchivos()
    {
        var archivos = new List<(int, string)>();
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT Id, NombreArchivo FROM Archivos";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    archivos.Add((reader.GetInt32(0), reader.GetString(1)));
                }
            }
        }
        return archivos;
    }
}
