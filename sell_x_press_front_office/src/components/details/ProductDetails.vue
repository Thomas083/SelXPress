<template>
    <div class="product-details">
        <h2 class="product-title">{{ product.name }}</h2>
        <div class="product-seller">Sold by: <span class="product-seller-name">SelXPress</span></div>
        <div class="product-price-cart-container">
            <div class="product-price">
                <h2 class="price">{{ product.price }} €</h2>
                <div>All prices include VAT</div>
            </div>
            <button class="btn btn-primary add-to-cart-btn" @click="addToCart()">
                <span>Add to cart</span>
                <img src="../../assets/Product/add-to-cart.png" alt="Add to cart" />
            </button>
        </div>
        <div v-for="(attribute, index) in attributes" :key="index" class="attributes-container">
            <div class="attribute-name">{{ attribute.name }}:</div>
            <div v-if="attribute.name === 'color'" class="attributes-container">
                <div class="color-attribute" v-for="data in attribute.attributeData" :style="{ backgroundColor: data.value }">
                </div>
            </div>
            <div v-else class="attributes-container">
                <div class="attribute" v-for="data in attribute.attributeData">
                    {{ data.value }}
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { POST } from '@/api/axios';
import { ENDPOINTS } from '@/api/endpoints';
import { createToast } from 'mosha-vue-toastify';

export default {
    name: 'ProductDetails',
    props: {
        product: {
            type: Object,
            required: true 
        },
        attributes: {
            type: Array,
            required: true
        }
    },
    data() {
        return {
            addProduct: {
                productId: null,
                quantity: 1
            },
        }
    },
    methods: {
        addToCart() {
            this.addProduct.productId = this.product.id;
            if (localStorage.getItem('user')) {
                POST(ENDPOINTS.CREATE_CART, this.addProduct, JSON.parse(localStorage.getItem('user')).token)
                .then(() => {
                    createToast({ title: 'Add to cart successful', description: 'Your cart was successfully added' }, { type: 'success', position: 'bottom-right' });
                })
                .catch(() => {
                    createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
                })
            } else createToast(`You need to be connected to add a product to cart !`, { type: 'danger', position: 'bottom-right' });

        },
    },
}
</script>

<style scoped>

.product-details {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    gap: 1rem;
    padding-left: 3rem;
    width: 70%;
}

.product-price-cart-container {
    display: flex;
    flex-direction: row;
    gap: 3rem;
}

.product-title {
    font-size: 2rem;
    word-wrap: break-word;
    text-align: justify;
}

.attribute:hover,
.attribute-selected {
    background-color: var(--main-blue);
}

.product-seller {
    color: var(--main-grey-separation);
    font-size: 1.3rem;
}

.product-seller-name {
    text-decoration: underline;
}

.product-price {
    text-align: start;
}

.price {
    font-size: 3.5rem;
}

.attributes-container {
    display: flex;
    flex-direction: row;
    gap: 1.5rem;
    align-items: center;
    justify-content: center;
}

.attribute-name {
    font-size: 3rem;
}

.add-to-cart-btn {
    padding-left: 1rem;
    padding-right: 1rem;
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 1rem;
    height: 10vh;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

img {
    height: 3rem;
}

.attribute {
    cursor: pointer;
    border-radius: 0.5rem;
    border: 1px solid var(--main-black);
    padding: 1rem;
    text-transform: uppercase;
}

.attribute-selected {
    background-color: var(--main-blue);
}

.color-attribute {
    cursor: pointer;
    border: 1px solid var(--main-black);
    width: 7vw;
    height: 5vh;
    border-radius: 10px;
}

.color-attribute-selected {
    border: 2px solid var(--main-blue);
}
</style>