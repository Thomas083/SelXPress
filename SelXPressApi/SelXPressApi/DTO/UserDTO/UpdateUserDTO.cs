namespace SelXPressApi.DTO.UserDTO
{
    /// <summary>
    /// DTO to update User
    /// </summary>
    public class UpdateUserDTO
    {
        /// <summary>
        /// The new Username of the user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The new Email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The new Password of the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Field to check if the password is good (confirm password)
        /// </summary>
        public string ConfirmPassword { get; set; }

    }
}
