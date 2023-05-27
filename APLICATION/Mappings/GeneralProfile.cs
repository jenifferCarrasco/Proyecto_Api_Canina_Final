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
            CreateMap<Caninos, CaninoDto>();
            #endregion

            #region Commands
            CreateMap<CreateCaninoCommand, Caninos>();
            #endregion
            //centro
            #region
            CreateMap<Centros, CentrosDto>();
            #endregion

            #region Commands
            CreateMap<CreateCentroCommand, Centros>();
            #endregion
            //citas
            #region
            CreateMap<Citas, CitasDto>();
            #endregion

            #region Commands
            CreateMap<CreateCitaCommand, Citas>();
            #endregion
            //vacunaciones
            #region
            CreateMap<Vacunaciones, VacunacionesDto>();
            #endregion

            #region Commands
            CreateMap<CreateVacunacionCommand, Vacunaciones>();
            #endregion
            //vacunas
            #region
            CreateMap<Vacunas, VacunasDto>();
            #endregion

            #region Commands
            CreateMap<CreateVacunaCommand, Vacunas>();
            #endregion

            #region 
            CreateMap<Usuarios, UsuarioDto>();
            #endregion

            //propietario
            #region
            CreateMap<Propietarios, PropietariosDto>();
            #endregion

            #region Commands
            CreateMap<CreatePropietarioCommand,Propietarios>();
            #endregion

            //vacunadores
            #region
            CreateMap<Vacunadores, VacunadoresDto>();
            #endregion

            #region Commands
            CreateMap<CreateVacunadoresCommand, Vacunadores>();
            #endregion
        }
    }
}
