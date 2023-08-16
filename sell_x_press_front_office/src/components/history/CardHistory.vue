<template>
    <div class="accordion" :id="'accordion-' + orderId">
        <div class="accordion-item">
            <h2 class="accordion-header">
                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                    :data-bs-target="'#collapse-' + orderId" aria-expanded="false" :aria-controls="'collapse-' + orderId">
                    <div class="order-btn-container">
                        <div>Ref: {{ order.id }}</div>
                        <div>Order from {{ order.date }}</div>
                        <div>{{ order.price }}€</div>
                    </div>
                </button>
            </h2>
            <div :id="'collapse-' + orderId" class="accordion-collapse collapse">
                <div class="separation-container">
                    <div class="separation"></div>
                </div>
                <div class="accordion-body order-container" v-for="(product, key) in order.products" :key="product.id">
                    <div class="order-content-container">
                        <img class="img" src="@/assets/categories-product-maquette-1.jpg" alt="product picture" />
                        <div class="title">
                            <p>{{ product.name }}</p>
                            <p>{{ product.description }}</p>
                        </div>
                        <div class="bold-text">x{{ product.quantity }}</div>
                        <div class="bold-text">{{ product.price }}€</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "CardHistory",
    props: {
        orderId: {
            type: Number,
            required: true,
        },
        order: {
            type: Object,
            required: true,
        },
    },
}
</script>

<style scoped>

.img {
  max-height: 90%;
  max-width: 20%;
  height: 20%;
}

.title {
  max-width: 45%;
  max-height: 70%;
  overflow-y: scroll;
  text-align: start;
  word-wrap: break-word;
  -ms-overflow-style: none; /* IE and Edge */
  scrollbar-width: none; /* Firefox */
}

.title::-webkit-scrollbar {
  display: none;
}

.accordion-item {
    width: 90vw;
    gap: 1rem;
}

.accordion-button {
    gap: 5vw;
}

.order-btn-container {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    width: -webkit-fill-available;
}

.order-container {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}

.order-content-container {
    display: flex;
    flex-direction: row;
    justify-content: space-around;
}

.bold-text {
    font-weight: bold;
}


.separation-container {
    display: flex;
    align-items: center;
    justify-content: center;
}

.separation {
    margin: 1rem;
    background-color: var(--main-grey-separation);
    height: 1.5px;
    width: 80vw;
}
</style>