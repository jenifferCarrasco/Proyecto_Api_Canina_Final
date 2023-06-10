using APLICATION.DTOs;
using APLICATION.Feauters.Canino.Commands.CreateCommand;
using APLICATION.Feauters.Centros.Commands.CreateCentroCommand;
using APLICATION.Feauters.Citas.Commands.CreateCitasCommand;
using APLICATION.Feauters.Vacunaciones.Commands.CreateVacunacionCommand;
using APLICATION.Feauters.Vacunadores.Commands.CreateCommand;
using APLICATION.Feauters.Vacunas.Commands.CreateVacunaCommand;
using AutoMapper;
using DOMAIN.Canina;
using DOMAIN.Canina.Entities;

namespace APLICATION.Mappings
{
	public class GeneralProfile : Profile
	{
		public GeneralProfile()
		{

			//canino
			#region
			CreateMap<Canino, CaninoDto>()
				.ForMember(X => X.Sexo, act => act.MapFrom(x=>x.Sexo.ToString()))
				.ForMember(X => X.Estatus, act => act.MapFrom(x => x.Estatus.ToString()));

			#endregion

			#region Commands
			CreateMap<CreateCaninoCommand, Canino>()
				.ForMember(X => X.Estatus, act => act.MapFrom(_ => Estados.Activo));
			#endregion


			//centro
			#region
			CreateMap<Centro, CentrosDto>();
			#endregion

			#region Commands
			CreateMap<CreateCentroCommand, Centro>()
				.ForMember(X => X.Estatus, act => act.MapFrom(_ => Estados.Activo));

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
			CreateMap<Propietario, PropietariosDto>()
				.ForMember(X => X.Sexo, act => act.MapFrom(x => x.Sexo.ToString()))
				.ForMember(X => X.Email, act => act.MapFrom(x => x.Usuario.Email));

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
