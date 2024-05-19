using jchusinS6.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace jchusinS6.Views;

public partial class vActuEliminar : ContentPage
{
    public vActuEliminar(Estudiante datos)
    {
        InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {

        try
        {
            var cliente = new HttpClient();
            var parametros = new Dictionary<string, string>
                {
                    { "codigo", txtCodigo.Text },
                    { "nombre", txtNombre.Text },
                    { "apellido", txtApellido.Text },
                    { "edad", txtEdad.Text }
                };

            var json = JsonConvert.SerializeObject(parametros);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PutAsync("http://192.168.100.227/moviles/wsEstudiantes.php", contenido);
            respuesta.EnsureSuccessStatusCode();

            await DisplayAlert("Éxito", "Estudiante actualizado correctamente.", "Cerrar");
            await Navigation.PopAsync();
            

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Error al actualizar el estudiante: " + ex.Message, "Cerrar");
        }

    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var cliente = new HttpClient();

            // No es necesario pasar un segundo argumento para DeleteAsync
            var respuesta = await cliente.DeleteAsync("http://192.168.100.227/moviles/wsEstudiantes.php");
            respuesta.EnsureSuccessStatusCode();

            await DisplayAlert("Éxito", "Estudiante eliminado correctamente.", "Cerrar");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Error al eliminar el estudiante: " + ex.Message, "Cerrar");
        }
    }
}