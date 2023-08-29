<template>
    <div class="product-list-container">
        <tags-list />
        <div class="products-cards" id="products">
            <product-card v-for="product in displayedProducts" :key="product.id" :product="product" />
        </div>
    </div>
    <pagination-component v-if="products.length > productsPerPage" :key="currentPage" :totalProducts="products.length"
        :products="products" :products-per-page="productsPerPage" :currentPage="currentPage"
        @page-changed="updateCurrentPage" />
</template>

<script>

import ProductCard from "@/components/products/ProductCard.vue";
import TagsList from "@/components/tags/TagsList.vue";
import PaginationComponent from "@/components/pagination/PaginationComponent.vue";

export default {
    name: 'ProductListView',
    components: {
        ProductCard,
        TagsList,
        PaginationComponent
    },
    data() {
        return {
            productsPerPage: 4,
            currentPage: 1,
            products: [
                {
                    id: 1,
                    publication_date: '20/06/2023',
                    name: 'SAMODA, Child, Baleine, T-Shirt',
                    description: 'T-Shirt for youngest with small whale, Available in sizes 6 to 1...',
                    price: '16,99',
                    author: 'SAMODA'
                },
                {
                    id: 2,
                    publication_date: '20/06/2023',
                    name: 'SAMODA, Child, Baleine, T-Shirt',
                    description: 'T-Shirt for youngest with small whale, Available in sizes 6 to 1...',
                    price: '16,99',
                    author: 'SAMODA'
                },
                {
                    id: 3,
                    publication_date: '20/06/2023',
                    name: 'SAMODA, Child, Baleine, T-Shirt',
                    description: 'T-Shirt for youngest with small whale, Available in sizes 6 to 1...',
                    price: '16,99',
                    author: 'SAMODA'
                },
                {
                    id: 4,
                    publication_date: '20/06/2023',
                    name: 'SAMODA, Child, Baleine, T-Shirt',
                    description: 'T-Shirt for youngest with small whale, Available in sizes 6 to 1...',
                    price: '16,99',
                    author: 'SAMODA'
                },
                {
                    id: 5,
                    publication_date: '20/06/2024',
                    name: 'SAMODA, Child, Baleine, T-Shirt',
                    description: 'T-Shirt for youngest with small whale, Available in sizes 6 to 1...',
                    price: '16,99',
                    author: 'SAMODA'
                },
            ]
        }
    },
    computed: {
        displayedProducts() {
            const startIndex = (this.currentPage - 1) * this.productsPerPage;
            const endIndex = startIndex + this.productsPerPage;
            return this.products.slice(startIndex, endIndex);
        },
    },
    methods: {
        updateCurrentPage(newPage) {
            this.currentPage = newPage;
        },
    }
}

</script>

<style scoped>
.product-list-container {
    padding: 2rem 1rem;
    display: flex;
    gap: 3rem;
}

.products-cards {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    gap: 2rem;
}
</style>