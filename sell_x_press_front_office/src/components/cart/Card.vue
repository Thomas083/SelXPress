<template>
  <div class="cart-card-container">
    <img
      class="img"
      src="@/assets/categories-product-maquette-1.jpg"
      alt="productproduct productproduct productproduct productproduct productproduct productproduct productpoduct "
    />
    <p class="title">
      Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
      tempor incididunt ut labore et dolore magna aliqua.
    </p>
    <div>
      <img class="button-img" src="@/assets/button/minus_button.png" />
      <input-component
        class="price-input"
        @input="updateQuantity"
        type="number"
      />
      <img class="button-img" src="@/assets/button/plus_button.png" />
    </div>
    <div>
      <p>{{ quantityPrice }}</p>
      <img class="button-img" src="@/assets/button/delete.png" />
    </div>
  </div>
</template>

<script>
import InputComponent from "@/components/global/InputComponent.vue";
export default {
  name: "CartCard",
  components: {
    InputComponent,
  },
  data() {
    return {
      img: "",
      title: "",
      quantity: 1,
      price: 5,
    };
  },
  computed: {
    quantityPrice() {
      return this.quantity * this.price;
    },
  },
  methods: {
    updateQuantity(e) {
      this.quantity = e;
    },
  },
};
</script>

<style scoped>
.cart-card-container {
  display: flex;
  justify-content: space-around;
  background-color: white;
  border-radius: 10px;
  width: 70vw;
  height: 20vh;
  align-items: center;
}

.img {
  max-height: 90%;
  max-width: 20%;
}

.title {
  max-width: 45%;
  max-height: 90%;
  overflow-y: scroll;
  text-align: start;
  -ms-overflow-style: none; /* IE and Edge */
  scrollbar-width: none; /* Firefox */
}

.title::-webkit-scrollbar {
  display: none;
}

.button-img {
  width: 1.5rem;
}

@media screen and (max-width: 430px) {
  .cart-card-container {
    width: 90vw; 
    height: 10vh;   
  }
}
</style>
