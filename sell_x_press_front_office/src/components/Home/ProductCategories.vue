<template>
    <div class="products-by-categories-container" v-if="selectedProduct.length !== 0">
        <div class="title-show-more-container">
            <h2>{{ category.name }}</h2>
            <button class="show-more-btn" v-on:click="goToCategory(category.id, category.name)">Show more</button>
        </div>
        <div class="products-container">
            <img v-for="product in selectedProduct" :src="product.picture" :key="product.id" v-on:click="goToProduct(product.id, product.name)" />
        </div>
    </div>
</template>

<script>
import { GET } from '@/api/axios';
import { ENDPOINTS } from '@/api/endpoints';

export default {
    name: 'ProductCategories',
    props: {
        category: {
            type: Object,
            required: true,
        },
    },
    data() {
        return {
            numProducts: 5,
            products: null
        }
    },
    methods: {
        shuffleArray(array) {
            for (let i = array.length - 1; i > 0; i--) {
                const j = Math.floor(Math.random() * (i + 1));
                [array[i], array[j]] = [array[j], array[i]];
            }
        },
        goToCategory(id, name) {
            this.$router.push({ path: `/products/${id}/${name}`})
        },
        goToProduct(id, name) {
            this.$router.push({ path: `/product/${id}/${name}`})
        }
    },
    computed: {
        selectedProduct() {
            const productList = [];
            if (this.products && this.products.length <= this.numProducts) {
                for (let i = 0; i < this.products.length; i++) {
                    if (this.products[i].category.id === this.category.id) productList.push(this.products[i]);             
                }
            } else if (this.products && this.products.length > this.numProducts) {
                for (let i = 0; i < this.products.length; i++) {
                    if (this.products[i].category.id === this.category.id) productList.push(this.products[i]);             
                }
                const shuffledProducts = [...productList];
                this.shuffleArray(shuffledProducts);
                return shuffledProducts.slice(0, this.numProducts);
            }
            return productList;
        }
    },
    mounted () {
        GET(ENDPOINTS.GET_ALL_PRODUCTS)
        .then((response) => {
            this.products = response.data;
        })
        .catch((error) => {
            console.dir(error);
        });
    },
}

</script>

<style scoped>
.products-by-categories-container {
    background-color: var(--main-white);
    margin: 1rem;
    border: none;
    border-radius: 10px;
    display: flex;
    flex-direction: column;
}

.title-show-more-container {
    display: flex;
    flex-direction: row;
    gap: 1rem;
    padding: 1rem;
    color: var(--main-black);
}

.show-more-btn {
    border: none;
    background-color: transparent;
    color: var(--main-grey-product);
}

.show-more-btn:hover {
    text-decoration: underline;
    opacity: 0.7;
}

.products-container {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    padding: 1rem;
    gap: 10vw;
}

.product {
    padding: 1rem;
}

img {
    height: 20vh;
    cursor: pointer;
}
</style>