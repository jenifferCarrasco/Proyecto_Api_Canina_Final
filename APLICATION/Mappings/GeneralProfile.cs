using APLICATION.DTOs;
using APLICATION.Feauters.Canino.Commands.CreateCommand;
using APLICATION.Feauters.Centros.Commands.CreateCentroCommand;
using APLICATION.Feauters.Citas.Commands.CreateCitasCommand;
using APLICATION.Feauters.Vacunadores.Commands.CreateCommand;
using APLICATION.Feauters.Vacunaciones.Commands.CreateVacunacionCommand;
using APLICATION.Feauters.Vacunas.Commands.CreateVacunaCommand;
using AutoMapper;
using DOMAIN.Canina.Entities;
using APLICATION.Feauters.Propietarios.Commands.CreateCommand;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            //canino
            #region
            CreateMap<Canino, CaninoDto>();
            #endregion

            #region Commands
            CreateMap<CreateCaninoCommand, Canino>();
            #endregion
            //centro
            #region
            CreateMap<Centro, CentrosDto>();
            #endregion

            #region Commands
            CreateMap<CreateCentroCommand, Centro>();
            #endregion
            //citas
            #region
            CreateMap<Cita, CitasDto>();
            #endregion

            #region Commands
            CreateMap<CreateCitaCommand, Cita>();
            #endregion
            //vacunaciones
            #region
            CreateMap<Vacunacion, VacunacionesDto>();
            #endregion

            #region Commands
            CreateMap<CreateVacunacionCommand, Vacunacion>();
            #endregion
            //vacunas
            #region
            CreateMap<Vacuna, VacunasDto>();
            #endregion

            #region Commands
            CreateMap<CreateVacunaCommand, Vacuna>();
            #endregion

            #region 
            CreateMap<Usuario, UsuarioDto>();
            #endregion

            //propietario
            #region
            CreateMap<Propietario, PropietariosDto>();
            #endregion

            #region Commands
            CreateMap<CreatePropietarioCommand,Propietario>();
            #endregion

            //vacunadores
            #region
            CreateMap<Vacunador, VacunadoresDto>();
            #endregion

            #region Commands
            CreateMap<CreateVacunadoresCommand, Vacunador>();
            #endregion
        }
    }
}
