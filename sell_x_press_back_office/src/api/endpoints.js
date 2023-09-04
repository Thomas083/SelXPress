export const ENDPOINTS = {
    // AUTH
    USER_LOGIN: '/api/User/auth/login', //(POST)

    // USER
    GET_ALL_USER: '/api/User', //(GET)
    GET_ONE_USER: '/api/User/email', //(GET)
    CREATE_USER: '/api/User', //(POST)
    LOGOUT_USER: '/api/User/logout', //(DELETE)
    UPDATE_USER: '/api/User/email', //(PUT)
    REFRESH_TOKEN: '/api/User/refreshToken', //(POST)

    //ATRIBUTES
    GET_ALL_ATTRIBUTES: '/api/Attribute', //(GET)

    //CATEGORIES
    GET_ALL_CATEGORIES: '/api/Category', //(GET)

    //PRODUCTS
    GET_ONE_PRODUCT: '/api/Product', // (GET) + add this.$route.params.id
    POST_PRODUCT: '/api/Product', //(POST)
}