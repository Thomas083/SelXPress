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
import { GET } from "@/api/axios";
import { ENDPOINTS } from "@/api/endpoints";

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
            products: [],
        }
    },
    computed: {
        displayedProducts() {
            if (this.$route.params.name && this.$route.params.name !== 'all') {
                var newProductList = this.setProductlist(this.$route.params.name)
                const startIndex = (this.currentPage - 1) * this.productsPerPage;
                const endIndex = startIndex + this.productsPerPage;
                return newProductList.slice(startIndex, endIndex);
            } else {
                const startIndex = (this.currentPage - 1) * this.productsPerPage;
                const endIndex = startIndex + this.productsPerPage;
                return this.products.slice(startIndex, endIndex);
            }
        },
    },
    methods: {
        updateCurrentPage(newPage) {
            this.currentPage = newPage;
        },
        setProductlist(searchTerm) {
            if (!searchTerm) {
                return this.tempProduct;
            } else {
                searchTerm = searchTerm.toLowerCase();
                return this.products.filter(product => {
                    const nameMatches = product.name.toLowerCase().includes(searchTerm);
                    const descriptionMatches = product.description.toLowerCase().includes(searchTerm);
                    return nameMatches || descriptionMatches;
                });
            }
        }
    },
    watch: {
        '$route.params.id': function (newId, oldId) {
            if (newId !== oldId) {
                this.products = [];
                GET(ENDPOINTS.GET_ALL_PRODUCTS)
                    .then((response) => {
                        for (let i = 0; i < response.data.length; i++) {
                            if (response.data[i].category.id == this.$route.params.id) this.products.push(response.data[i])
                        };
                    });
            }
        }
    },
    mounted() {
        GET(ENDPOINTS.GET_ALL_PRODUCTS)
            .then((response) => {
                for (let i = 0; i < response.data.length; i++) {
                    if (response.data[i].category.id == this.$route.params.id) this.products.push(response.data[i])
                };
            });
    },
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