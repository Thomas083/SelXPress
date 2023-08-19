<template>
    <div class="history-container">
        <h1 class="order-history-title">Order History</h1>
        <div class="history-list-container">
            <div v-for="(order, key) in order_history" :key="order.id">
                <card-history :orderId="key" :order="order" />
            </div>
        </div>
    </div>
</template>
  
<script>
import CardHistory from "@/components/history/CardHistory.vue";

export default {
    name: 'HomeView',
    components: {
        CardHistory,
    },
    data() {
        return {
            // WIP : to be replaced by the cart from the API
            order_history: [
                {
                    id: 1,
                    date: '20/06/2023',
                    price: 10,
                    products: [
                        {
                            quantity: 1,
                            id: 1,
                            name: "Frite au kilos - A la belge",
                            price: 10,
                            picture: "@/assets/categories-product-maquette-1.jpg",
                            description: "description1",
                        },
                        {
                            quantity: 2,
                            id: 2,
                            name: "Glace au litre - 3 parfums max",
                            price: 20,
                            picture: "@/assets/categories-product-maquette-1.jpg",
                            description: "description2",
                        },
                        {
                            quantity: 3,
                            id: 3,
                            name: "name3",
                            price: 30,
                            picture: "@/assets/categories-product-maquette-1.jpg",
                            description: "description3",
                        },
                    ]
                },
                {
                    id: 2,
                    date: '18/06/2023',
                    price: 20,
                    products: [
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
                },
            ]
        }
    },
}
</script>
  
<style>

.accordion-item {
    width: 90vw;
    gap: 1rem;
}

.history-container {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}

.history-list-container {
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    align-items: center;
    gap: 1rem;
}

.order-history-title {
    text-align: center;
    text-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
    font-size: 50px;
    font-weight: 600;
    line-height: normal;
}
</style>