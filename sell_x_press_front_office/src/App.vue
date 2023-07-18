<template>
  <header-identification-page v-if="isIdentificationPage" />
  <header-registered v-else-if="isUserLoggedIn" />
  <header-not-registered v-else />
  <router-view />
  <footer-layout />
</template>

<script>
// @ is an alias to /src
import HeaderNotRegistered from '@/components/Layout/HeaderNotRegistered.vue';
import HeaderRegistered from '@/components/Layout/HeaderRegitered.vue';
import HeaderIdentificationPage from '@/components/Layout/HeaderIdentificationPage.vue';
import FooterLayout from '@/components/Layout/FooterLayout.vue';
import ProductCategories from '@/components/Home/ProductCategories.vue';

export default {
  name: 'Appvue',
  components: {
    HeaderNotRegistered,
    HeaderRegistered,
    HeaderIdentificationPage,
    FooterLayout,
    ProductCategories,
  },
  computed: {
    isIdentificationPage() {
      return (this.$route.path === '/login' || this.$route.path === '/register' || this.$route.path === '/forgot')
    },
    isUserLoggedIn() {
      return (localStorage.getItem("email") != null);
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
