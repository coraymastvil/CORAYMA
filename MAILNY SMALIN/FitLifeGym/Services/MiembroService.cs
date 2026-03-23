using System.Collections.Generic;
using FitLifeGym.Models;
using FitLifeGym.Repository;

namespace FitLifeGym.Services
{
    public interface IMiembroService
    {
        void Registrar(Miembro miembro);
        List<Miembro> ObtenerTodos();
        Miembro? BuscarPorCedula(string cedula);
        void ActualizarTel(string cedula, string tel);
        void BorrarMiembro(string cedula);
    }

    public class MiembroService : IMiembroService
    {
        private readonly IMiembroRepository _repository;

        public MiembroService(IMiembroRepository repository)
        {
            _repository = repository;
        }

        public void Registrar(Miembro miembro) => _repository.RegistrarMiembro(miembro);
        public List<Miembro> ObtenerTodos() => _repository.ListarTodos();
        public Miembro? BuscarPorCedula(string cedula) => _repository.BuscarPorCedula(cedula);
        public void ActualizarTel(string cedula, string tel) => _repository.ActualizarTelefono(cedula, tel);
        public void BorrarMiembro(string cedula) => _repository.EliminarMiembro(cedula);
    }
}
