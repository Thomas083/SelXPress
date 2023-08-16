<template>
  <div class="home-container">
    <categories-list />
    <div class="products-cards-container">
      <div class="search-product">
        <h1 class="products-category" id="category">Category: All</h1>
        <filter-product />
      </div>
      <div class="products-cards">
        <product-card v-for="product in displayedProducts" :key="product.id" :product="product" />
      </div>
    </div>
  </div>
  <pagination v-if="products.length > productsPerPage" :key="currentPage" :totalProducts="products.length" :products="products" :products-per-page="productsPerPage" :currentPage="currentPage" @page-changed="updateCurrentPage" />
</template>

<script>

import CategoriesList from "@/components/categories/CategoriesList.vue"
import ProductCard from "@/components/products/ProductsCard.vue"
import Pagination from "@/components/pagination/Pagination.vue"
import FilterProduct from "@/components/filter/FilterProduct.vue"

export default {
  name: 'HomeView',
  components: {
    ProductCard,
    CategoriesList,
    Pagination,
    FilterProduct
  },
  data() {
    return {
      productsPerPage: 15,
      currentPage: 1,
      products: [
        {
          id: 1,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        }, {
          id: 2,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        }, {
          id: 3,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        },
        {
          id: 4,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        },
        {
          id: 5,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        },
        {
          id: 6,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        },
        {
          id: 7,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        },
        {
          id: 8,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        },
        {
          id: 9,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        },
        {
          id: 10,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        },
        {
          id: 11,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        },
        {
          id: 12,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        },
        {
          id: 13,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
        },
        {
          id: 13,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          publication_date: "20/06/2023",
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

<style>
.home-container {
  padding: 2rem 1rem;
  display: flex;
  gap: 3rem;
}

.products-cards-container {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  gap: 1rem;
}

.products-category {
  font-weight: bold;
}

.products-cards {
  display: flex;
  flex-wrap: wrap;
  gap: 2rem;
}

.search-product {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
</style>
