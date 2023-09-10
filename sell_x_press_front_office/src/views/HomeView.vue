<template>
  <home-carrousel />
  <product-categories v-for="category in selectedCategories" :category="category" />
</template>

<script>
// @ is an alias to /src
import { GET } from '@/api/axios';
import { ENDPOINTS } from '@/api/endpoints';
import HomeCarrousel from '@/components/Home/HomeCarrousel.vue';
import ProductCategories from '@/components/Home/ProductCategories.vue';

export default {
  name: 'HomeView',
  data() {
    return {
      categories: null,
      numCategories: 3,
    }
  },
  components: {
    HomeCarrousel,
    ProductCategories,
  },
  methods: {
    shuffleArray(array) {
      for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
      }
    },
  },
  computed: {
    selectedCategories() {
      if (this.categories && this.categories.length <= this.numCategories) {
        return this.categories;
      } else if (this.categories && this.categories.length > this.numCategories) {
        const shuffledCategories = [...this.categories];
        this.shuffleArray(shuffledCategories);
        return shuffledCategories.slice(0, this.numCategories);
      }
    }
  },
  mounted() {
    GET(ENDPOINTS.GET_ALL_CATEGORIES)
      .then((response) => {
        this.categories = response.data
      })
      .catch((error) => {
        console.dir(error)
      });
  },
}
</script>
