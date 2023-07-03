<template>
  <Header />
  <Footer/>
  <Help />
</template>

<script>
// @ is an alias to /src
import Header from '@/components/Layout/Header.vue';
import Footer from '@/components/Layout/Footer.vue';
import Help from '@/components/Layout/Help.vue'

export default {
  name: 'HomeView',
  components: {
    Header,
    Footer,
    Help
  }
}
</script>
