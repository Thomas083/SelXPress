<template>
    <h1>Your Cart</h1>
    <div class="cart-container">
        <div class="cart-list-container">
            <div v-for="(item, key) in cart" :key="item.id">
                <cart-card :cardId="key" :cart="item" @delete="deleteItem" @update-quantity="updateQuantity" />
            </div>
        </div>
        <div class="cart-summary">
            <cart-summary :cart="cart" />
        </div>
    </div>
</template>

<script>
import CartCard from "@/components/cart/Card.vue";
import CartSummary from "@/components/cart/Summary.vue";
import { GET } from "@/api/axios";
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
        deleteItem(id) {
            this.cart.splice(id, 1);
        },
        updateQuantity([id, quantity]) {
            this.cart[id].quantity = quantity;
        }
    },
    mounted() {
        GET(ENDPOINTS.GET_ONE_USER, JSON.parse(localStorage.getItem('user')).token)
            .then((response) => {
                GET(ENDPOINTS.GET_MY_CART + `/${response.data.id}/user`, JSON.parse(localStorage.getItem('user')).token)
                    .then((response) => {
                        this.cart = response.data
                    })
                    .catch((error) => {
                        console.dir(error)
                    });
            })
            .catch((error) => {
                console.dir(error)
            });
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