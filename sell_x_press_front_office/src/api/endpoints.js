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

    // CATEGORIES
    GET_ALL_CATEGORIES: '/api/Category', //(GET)
    GET_ONE_CATEGORY: '/api/Category/:id', //(GET)
    UPDATE_CATEGORY: '/api/Category/:id', //(PUT)

    // PRODUCTS
    GET_ALL_PRODUCTS: '/api/Product', //(GET)
    GET_ALL_CATEGORY_PRODUCTS: '/api/Product/:name', //(GET)
    GET_ONE_PRODUCT: '/api/Product/:id', //(GET)
    UPDATE_PRODUCT: '/api/Product/:id', //(PUT)
}
