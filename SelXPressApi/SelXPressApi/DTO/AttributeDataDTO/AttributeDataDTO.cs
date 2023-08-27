﻿namespace SelXPressApi.DTO.AttributeDataDTO
{
    /// <summary>
    /// Data transfer object representing additional data associated with an attribute.
    /// </summary>
    public class AttributeDataDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the attribute data.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the key associated with the attribute data.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value associated with the attribute data.
        /// </summary>
        public string Value { get; set; }
    }
}
