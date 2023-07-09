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

			CreateMap<Canino, CaninoDto>()
				.ForMember(X => X.Sexo, act => act.MapFrom(x => x.Sexo.ToString()))
				.ForMember(X => X.Estatus, act => act.MapFrom(x => x.Estatus.ToString()));

			CreateMap<CreateCaninoCommand, Canino>()
				.ForMember(X => X.Estatus, act => act.MapFrom(_ => Estados.Activo));


			//centro
			#region
			CreateMap<Centro, CentrosDto>()
			.ForMember(X => X.Estado, act => act.MapFrom(x => x.Estatus));
			#endregion

			#region Commands
			CreateMap<CreateCentroCommand, Centro>()
				.ForMember(X => X.Estatus, act => act.MapFrom(_ => Estados.Activo));

			#endregion


			CreateMap<Cita, CitasDto>();
			CreateMap<Canino, CitasCaninoDto>();
			CreateMap<Vacunador, CitasVacunadorDto>();
			CreateMap<Centro, CitasCentroDto>();
			CreateMap<Propietario, CitasPropietarioDto>();
			CreateMap<CreateCitaCommand, Cita>()
				.ForMember(x => x.Estatus, act => act.MapFrom(_ => Estados.Activo));


			CreateMap<Vacunacion, VacunacionesDto>();
			CreateMap<Canino, VacunacionCaninoDto>();
			CreateMap<Vacunador, VacunacionVacunadorDto>()
				.ForMember(x => x.Nombre, src => src.MapFrom(x => $"{x.Nombre} {x.Apellido}"));
			CreateMap<Vacuna, VacunacionVacunaDto>();
			CreateMap<Centro, VacunacionCentroDto>();


			CreateMap<CreateVacunacionCommand, Vacunacion>()
				.ForMember(x => x.Dosis, src => src.MapFrom(x => ObtenerDosis(x.Dosis)));

			CreateMap<Vacuna, VacunasDto>()
				.ForMember(x => x.Nombre, src => src.MapFrom(x => $"{x.Nombre}-{x.Lote}"))
				.ForMember(x => x.CantidadDisponible,
				src => src.MapFrom(x => x.VacunaInventario.CantidadDisponible))
				.ForMember(x => x.Estado, src => src.MapFrom(x => x.Estatus));

			CreateMap<CreateVacunaCommand, Vacuna>()
				.ForMember(x => x.Lote, dest => dest.MapFrom(x => x.Lote.ToUpper()));


			CreateMap<Usuario, UsuarioDto>();

			//propietarios
			CreateMap<Propietario, PropietariosDto>()
				.ForMember(X => X.Email, act => act.MapFrom(x => x.Usuario.Email))
				.ForMember(x=>x.Username, src=>src.MapFrom(x=>x.Usuario.UserName));

			//vacunadores
			CreateMap<Vacunador, VacunadoresDto>();
			CreateMap<CreateVacunadoresCommand, Vacunador>();


			//admin
			CreateMap<Administrador, AdministradoresDto>()
				.ForMember(x => x.Email, src => src.MapFrom(x => x.Usuario.Email))
				.ForMember(x => x.Username, src => src.MapFrom(x => x.Usuario.UserName))
				.ForMember(x => x.UsuarioId, src => src.MapFrom(x => x.Usuario.Id))
				.ForMember(x => x.TipoUsuario, src => src.MapFrom(x => x.Usuario.TipoUsuario));


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
