<template>
  <header-not-registered v-if="!isIdentificationPage" />
  <header-regitered v-else-if="isUserLoggedIn" />
  <header-identification-page v-else />
  <router-view />
  <footer-layout />
</template>

<script>
// @ is an alias to /src
import HeaderNotRegistered from '@/components/Layout/HeaderNotRegistered.vue';
import HeaderRegitered from '@/components/Layout/HeaderRegitered.vue';
import HeaderIdentificationPage from '@/components/Layout/HeaderIdentificationPage.vue';
import FooterLayout from '@/components/Layout/FooterLayout.vue';
import ProductCategories from '@/components/Home/ProductCategories.vue';
export default {
  name: 'Appvue',
  components: {
    HeaderNotRegistered,
    HeaderRegitered,
    HeaderIdentificationPage,
    FooterLayout,
    ProductCategories,
  },
  computed: {
    isIdentificationPage() {
      if (this.$route.path === '/login' || this.$route.path === 'register') return true;
    },
    isUserLoggedIn() {
      return false;
    },
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  background-color: #E5E5E5;
}

nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #42b983;
}
</style>
