<template>
  <header-not-registered />
  <!-- <nav>
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link> |
    <router-link to="/login">Login</router-link>
  </nav> -->
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
