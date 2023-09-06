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
import { GET } from "@/api/axios";
import { ENDPOINTS } from "@/api/endpoints";
import CardHistory from "@/components/history/CardHistory.vue";

export default {
    name: 'HistoryView',
    components: {
        CardHistory,
    },
    data() {
        return {
            // WIP : to be replaced by the cart from the API
            order_history: []
        }
    },
    mounted() {
        GET(ENDPOINTS.GET_MY_ORDER, JSON.parse(localStorage.getItem('user')).token)
            .then((response) => {
                this.order_history = response.data
            })
            .catch((error) => { console.dir(error) });
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