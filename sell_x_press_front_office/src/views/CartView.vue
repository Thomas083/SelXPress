<template>
    <h1>Your Cart</h1>
    <div class="cart-container">
        <div class="cart-list-container">
            <div v-for="(item, key) in cart" :key="item.id">
                <cart-card :cardId="key" :cart="item" @delete="deleteItem" @update-quantity="updateQuantity" />
            </div>
        </div>
        <div class="cart-summary">
            <cart-summary :cart="cart" @refreshCart="getMyCart" />
        </div>
    </div>
</template>

<script>
import CartCard from "@/components/cart/Card.vue";
import CartSummary from "@/components/cart/Summary.vue";
import { GET, PUT, DELETE } from "@/api/axios";
import { ENDPOINTS } from "@/api/endpoints";

export default {
    name: "CartView",
    components: {
        CartCard,
        CartSummary
    },
    data() {
        return {
            cart: []
        }
    },
    methods: {
        deleteItem(index) {
            DELETE(ENDPOINTS.GET_MY_CART + `/${this.cart[index].id}`, JSON.parse(localStorage.getItem('user')).token)
                    .then((response) => {
                        this.cart.splice(index, 1);
                    })
                    .catch((error) => {
                        console.dir(error)
                    });
        },
        updateQuantity([index, quantity]) {
            PUT(ENDPOINTS.GET_MY_CART + `/${this.cart[index].id}`, {"quantity":quantity}, JSON.parse(localStorage.getItem('user')).token)
                    .then((response) => {
                        this.cart[index].quantity = quantity;
                    })
                    .catch((error) => {
                        console.dir(error)
                    });            
        },
        getMyCart() {
            GET(ENDPOINTS.GET_ONE_USER, JSON.parse(localStorage.getItem('user')).token)
            .then((response) => {
                GET(ENDPOINTS.GET_MY_CART + `/${response.data.id}/user`, JSON.parse(localStorage.getItem('user')).token)
                    .then((response) => {
                        this.cart = response.data
                    })
                    .catch((error) => {
                        this.cart = []
                        console.dir(error)
                    });
            })
            .catch((error) => {
                console.dir(error)
            });
        }
    },
    created() {
        this.getMyCart()
    },
}
</script>

<style scoped>
.cart-container {
    display: flex;
    justify-content: center;
    gap: 1rem;
}

.cart-list-container {
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    align-items: center;
    gap: 1rem;
}

.cart-summary {
    display: flex;
}


@media screen and (max-width: 430px) {
    .cart-container {
        flex-direction: column;
    }

    .cart-summary {
        order: -1;
        justify-content: center;
        align-items: center;
    }
}
</style>