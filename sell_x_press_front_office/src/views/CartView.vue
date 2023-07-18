<template>
    <h1>Your Cart</h1>
    <div class="cart-container">
        <div class="cart-list-container">
            <cart-card/>
            <cart-card/>
            <cart-card/>
        </div>
        <div class="cart-summary">
            <cart-summary/>
        </div>
    </div>
</template>

<script>
import CartCard from "@/components/cart/Card.vue";
import CartSummary from "@/components/cart/Summary.vue";
    export default {
        name: "CartView",
        components: {
            CartCard,
            CartSummary
        },
        data() {
            return {
                cart: {
                    0: {
                        id: 1,
                        name: "name1",
                        price: 10,
                        picture: "https://picsum.photos/200/300",
                        quantity: 1,
                    },
                    1: {
                        id: 2,
                        name: "name2",
                        price: 20,
                        picture: "https://picsum.photos/200/300",
                        quantity: 2,
                    },
                    2: {
                        id: 3,
                        name: "name3",
                        price: 30,
                        picture: "https://picsum.photos/200/300",
                        quantity: 3,
                    },
                }
            }
        },
    }
</script>

<style scoped>
.cart-container{
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

@media screen and (max-width: 430px) {
        .cart-container{
        flex-direction: column;
    }

    .cart-summary {
        order: -1;
    }
}
</style>