﻿using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.DTO.AttributeDataDTO
{
    public class AttributeDataDTO
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Attribute Attribute { get; set; }
    }
}
