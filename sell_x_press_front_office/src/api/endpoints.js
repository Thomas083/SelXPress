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
    GET_ONE_CATEGORY: '/api/Category/', //(GET) and add + this.$route.params.id
    UPDATE_CATEGORY: '/api/Category/', //(PUT) and add + this.$route.params.id

    // TAGS
    GET_ALL_TAGS: '/api/Tag', // (GET)

    // PRODUCTS
    GET_ALL_PRODUCTS: '/api/Product', //(GET)
    GET_ALL_CATEGORY_PRODUCTS: '/api/Product/:name', //(GET)
    GET_ONE_PRODUCT: '/api/Product/', //(GET) and add + this.$route.params.id
    UPDATE_PRODUCT: '/api/Product/', //(PUT) and add + this.$route.params.id

    // COMMENTS
    GET_ALL_COMMENT: '/api/Comment', //(GET)
    GET_ONE_COMMENT: '/api/Comment', // (GET) + add comment id
    CREATE_COMMENT: '/api/Comment', //(POST)

    // CART
    GET_ALL_CART: '/api/Cart', //(GET)
    GET_MY_CART: '/api/Cart', //(GET)
    CREATE_CART: '/api/Cart/me', //(POST)

    // ORDER
    GET_MY_ORDER: '/api/Order/me', // (GET)
    CREATE_ORDER: '/api/Order', //(POST)

    // ATTRIBUTE
    GET_ONE_ATTRIBUTE: '/api/Attribute', // (GET) + add id attribute
}
