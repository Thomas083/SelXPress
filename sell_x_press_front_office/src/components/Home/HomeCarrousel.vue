<template>
    <div id="carouselExample" class="carousel carousel-dark slide" data-bs-ride="carousel">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img v-for="product in carrouselProduct.slice(0,Math.ceil(carrouselProduct.length/3))" :src="product.picture" v-on:click="goToProduct(product.id, product.name)" alt="...">
            </div>
            <div class="carousel-item">
                <img v-for="product in carrouselProduct.slice(Math.ceil(carrouselProduct.length/3),Math.ceil(carrouselProduct.length/6)+Math.ceil(carrouselProduct.length/3)+1)" v-on:click="goToProduct(product.id, product.name)" :src="product.picture" alt="...">
            </div>
            <div class="carousel-item">
                <img v-for="product in carrouselProduct.slice(Math.ceil(carrouselProduct.length/6)+Math.ceil(carrouselProduct.length/3)+1,carrouselProduct.length)" v-on:click="goToProduct(product.id, product.name)" :src="product.picture" alt="...">
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>
</template>

<script>

import { GET } from '@/api/axios';
import { ENDPOINTS } from '@/api/endpoints';

export default {
    name: 'HomeCarrousel',
    data() {
        return {
            products: null,
            numProduct: 12,
        }
    },
    methods: {
        shuffleArray(array) {
            for (let i = array.length - 1; i > 0; i--) {
                const j = Math.floor(Math.random() * (i + 1));
                [array[i], array[j]] = [array[j], array[i]];
            }
        },
        goToProduct(id, name) {
            this.$router.push({ path: `/product/${id}/${name}`})
        }
    },
    computed: {
        carrouselProduct() {
            if (this.products && this.products.length <= this.numProduct) {
                return this.products;
            } else if (this.products && this.products.length > this.numProduct) {
                const shuffledProducts = [...this.products];
                this.shuffleArray(shuffledProducts);
                return shuffledProducts.slice(0, this.numProduct);
            } else return []
        }
    },
    mounted() {
        GET(ENDPOINTS.GET_ALL_PRODUCTS)
            .then((response) => {
                this.products = response.data
            })
            .catch((error) => {
                console.dir(error)
            });
    },
}
</script>

<style scoped>
.carousel-item.active {
    display: flex;
    flex-direction: row;
    justify-content: center;
    flex-wrap: wrap;
    gap: 10vw;
}

.carousel {
    background-color: var(--main-white);
    margin: 5rem;
    padding: 2rem 0;
    border: none;
    border-radius: 10px;
    display: flex;
    flex-direction: column;
}

img {
    height: 20vh;
    cursor: pointer;
}
</style>