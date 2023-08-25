<template>
    <div class="products-by-categories-container">
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
            products: [
                {
                    id: 0,
                    name: 'test0',
                    picture: "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C81OjqS8lfCL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UX679_.png",
                },
                {
                    id: 1,
                    name: 'test1',
                    picture: "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C81OjqS8lfCL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UX679_.png",
                },
                {
                    id: 2,
                    name: 'test2',
                    picture: "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C81OjqS8lfCL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UX679_.png",
                },
                {
                    id: 3,
                    name: 'test3',
                    picture: "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C81OjqS8lfCL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UX679_.png",
                },
                {
                    id: 4,
                    name: 'test4',
                    picture: "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C81OjqS8lfCL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UX679_.png",
                },
                {
                    id: 5,
                    name: 'test5',
                    picture: "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C81OjqS8lfCL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UX679_.png",
                },
                {
                    id: 6,
                    name: 'test6',
                    picture: "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C81OjqS8lfCL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UX679_.png",
                },
                {
                    id: 7,
                    name: 'test7',
                    picture: "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C81OjqS8lfCL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UX679_.png",
                },
                {
                    id: 8,
                    name: 'test8',
                    picture: "https://m.media-amazon.com/images/I/A13usaonutL._CLa%7C2140%2C2000%7C81OjqS8lfCL.png%7C0%2C0%2C2140%2C2000%2B0.0%2C0.0%2C2140.0%2C2000.0_AC_UX679_.png",
                },
            ]
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
            if (this.products && this.products.length <= this.numProducts) {
                return this.products;
            } else if (this.products && this.products.length > this.numProducts) {
                const shuffledProducts = [...this.products];
                this.shuffleArray(shuffledProducts);
                return shuffledProducts.slice(0, this.numProducts);
            }
        }
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