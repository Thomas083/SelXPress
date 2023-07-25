<template>
    <h1>Your Cart</h1>
    <div class="cart-container">
        <div class="cart-list-container">
            <div v-for="(item, key) in cart" :key="item.id">
                <cart-card :cardId="key" :cart="item" @delete="deleteItem" @update-quantity="updateQuantity"/>
            </div>
        </div>
        <div class="cart-summary">
            <cart-summary :cart="cart"/>
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
                // WIP : to be replaced by the cart from the API
                cart: [
                    {
                        quantity: 1,
                        id: 1,
                        name: "name1",
                        price: 10,
                        picture: "@/assets/button/minus_button.png",
                        description: "description1",
                    },
                    {
                        quantity: 2,
                        id: 2,
                        name: "name2",
                        price: 20,
                        picture: "@/assets/button/minus_button.png",
                        description: "description2",
                    },
                    {
                        quantity: 3,
                        id: 3,
                        name: "name3",
                        price: 30,
                        picture: "@/assets/button/minus_button.png",
                        description: "description3",
                    },
                ]
            }
        },
        methods: {
            deleteItem(id) {
                this.cart.splice(id, 1);
            },
            updateQuantity([id, quantity]) {
                this.cart[id].quantity = quantity;
                console.log(this.cart[id]);
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

.cart-summary {
    display: flex;
}


@media screen and (max-width: 430px) {
    .cart-container{
        flex-direction: column;
    }

    .cart-summary {
        order: -1;
        justify-content: center;
        align-items: center;
    }
}
</style>