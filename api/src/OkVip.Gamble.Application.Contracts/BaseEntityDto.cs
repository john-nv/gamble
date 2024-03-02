using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace OkVip.Gamble
{
	public class BaseEntityDto<Tkey> : EntityDto<Tkey>, IHasExtraProperties
	{
		private ExtraPropertyDictionary _extraProperties;

		public DateTime CreationTime { get; set; }

		public DateTime? LastModificationTime { get; set; }

		public ExtraPropertyDictionary ExtraProperties
		{
			get => _extraProperties ??= [];
			set => _extraProperties = value;
		}
	}
}
