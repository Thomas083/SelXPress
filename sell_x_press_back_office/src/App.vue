<template>
  <header-identification-page v-if="isIdentificationPage"  />
  <header-admin v-else-if="isUserAdmin" />
  <header-not-admin v-else/>
  <router-view/>
  <footer-layout />
</template>

<script>
// @ is an alias to /src
import HeaderIdentificationPage from '@/components/Layout/HeaderIdentificationPage.vue';
import HeaderNotAdmin from '@/components/Layout/HeaderNotAdmin.vue';
import HeaderAdmin from '@/components/Layout/HeaderAdmin.vue';
import FooterLayout from '@/components/Layout/FooterLayout.vue';
import { GET } from '@/api/axios';
import { ENDPOINTS } from './api/endpoints';

export default {
  components: {
    HeaderIdentificationPage,
    HeaderNotAdmin,
    HeaderAdmin,
    FooterLayout
  },
  data() {
    return {
      userRole: null
    }
  },
  methods: {
    fetchUserRole() {
      const userToken = JSON.parse(localStorage.getItem('user')).token;
      GET(ENDPOINTS.GET_ONE_USER, userToken)
        .then((response) => {
          this.userRole = response.data.role.id;
        })
        .catch((error) => {
          console.error(error);
        });
    },
    },
  computed: {
    isIdentificationPage() {
      return (this.$route.path === '/login' || this.$route.path === '/register' || this.$route.path === '/forgot')
    },
    isUserAdmin() {
      return this.userRole === 3;
    },
  },
  mounted () {
    this.fetchUserRole();
  },
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
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
