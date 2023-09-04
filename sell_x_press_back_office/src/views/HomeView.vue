<template>
  <div class="home-container">
    <categories-list @categoryChoose="updateCategory($event)"/>
    <div class="products-cards-container">
      <div class="search-product">
        <h1 class="products-category" id="category">Category: {{ category}}</h1>
        <filter-product />
      </div>
      <div class="products-cards">
        <product-card v-for="product in products" :key="product.id" :product="product" />
      </div>
    </div>
  </div>
</template>

<script>

import CategoriesList from "@/components/categories/CategoriesList.vue"
import ProductCard from "@/components/products/ProductsCard.vue"
import FilterProduct from "@/components/filter/FilterProduct.vue"

export default {
  name: 'HomeView',
  components: {
    ProductCard,
    CategoriesList,
    FilterProduct
  },
  data() {
    return {
      category: 'All',
      products: [
        {
          id: 1,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: '2023-08-22T11:15:50.635Z',
        }, {
          id: 2,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        }, {
          id: 3,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
        {
          id: 4,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
        {
          id: 5,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
        {
          id: 6,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
        {
          id: 7,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
        {
          id: 8,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
        {
          id: 9,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
        {
          id: 10,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
        {
          id: 11,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
        {
          id: 12,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
        {
          id: 13,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
        {
          id: 13,
          name: "SAMODA, Child, Baleine, T-Shirt",
          price: '16,99',
          createdAt: "2023-08-22T11:15:50.635Z",
        },
      ]
    }
  },
  methods: {
    updateCurrentPage(newPage) {
      this.currentPage = newPage;
    },
    updateCategory(categoryChoose) {
      this.category = categoryChoose.name
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
