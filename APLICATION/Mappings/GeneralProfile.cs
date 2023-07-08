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
				.ForMember(X => X.Sexo, act => act.MapFrom(x => x.Sexo.ToString()))
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
			CreateMap<Canino, CitasCaninoDto>();
			CreateMap<Vacunador, CitasVacunadorDto>();
			CreateMap<Centro, CitasCentroDto>();
			CreateMap<Propietario, CitasPropietarioDto>();

			#endregion

			#region Commands
			CreateMap<CreateCitaCommand, Cita>()
				.ForMember(x => x.Estatus, act => act.MapFrom(_ => Estados.Activo));
			#endregion
			//vacunaciones
			#region
			CreateMap<Vacunacion, VacunacionesDto>();
			CreateMap<Canino, VacunacionCaninoDto>();
			CreateMap<Vacunador, VacunacionVacunadorDto>()
				.ForMember(x => x.Nombre, src => src.MapFrom(x => $"{x.Nombre} {x.Apellido}"));
			CreateMap<Vacuna, VacunacionVacunaDto>();
			CreateMap<Centro, VacunacionCentroDto>();
			#endregion

			//vacunas

			#region Commands

			CreateMap<CreateVacunacionCommand, Vacunacion>()
				.ForMember(x => x.Dosis, src => src.MapFrom(x => ObtenerDosis(x.Dosis)));



			#endregion

			#region

			CreateMap<Vacuna, VacunasDto>()
				.ForMember(x => x.Nombre, dest => dest.MapFrom(x => $"{x.Nombre}-{x.Lote}"))
				.ForMember(x => x.CantidadDisponible,
				dest => dest.MapFrom(x => x.VacunaInventario.CantidadDisponible));

			#endregion

			#region Commands

			CreateMap<CreateVacunaCommand, Vacuna>()
				.ForMember(x => x.Lote, dest => dest.MapFrom(x => x.Lote.ToUpper()))
				.ForMember(x => x.Estatus, dest => dest.MapFrom(_ => Estados.Activo));

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
		private string ObtenerDosis(int dosis)
		{
			return dosis switch
			{
				1 => "1ra",
				2 => "2da",
				3 => "3ra",
				4 => "4ta",
				5 => "5ta",
				_ => string.Empty
			};
		}
	}


}
