<template>
    <div>
        CartSummary 
        totalPrice : {{ totalPrice }}
        totalQuantity : {{ totalQuantity }}
    </div>
</template>

<script>
    export default {
        name: "CartSummary",
        props: {
            cart: {
                type: Array,
                required: true,
            },
        },
        data() {
            return {
                totalQuantity: this.cart.reduce((acc, item) => acc + item.quantity, 0),
                totalPrice: this.cart.reduce((acc, item) => acc + item.price * item.quantity, 0),
            }
        },
        // computed: {
        //     totalQuantity() {
        //         return this.cart.reduce((acc, item) => acc + item.quantity, 0); 
        //     },
        //     totalPrice() {
        //         return this.cart.reduce((acc, item) => acc + item.price * item.quantity, 0);
        //     },
        // },
        watch: {
            cart(newValue, oldValue) {
                this.totalQuantity = newValue.reduce((acc, item) => acc + item.quantity, 0);
                this.totalPrice = newValue.reduce((acc, item) => acc + item.price * item.quantity, 0);
                
            }
        },
    }
</script>

<style scoped>

</style>