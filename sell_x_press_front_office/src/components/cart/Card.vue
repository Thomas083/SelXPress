<template>
  <div class="cart-card-container">
    <img
      class="img"
      src="@/assets/categories-product-maquette-1.jpg"
      alt="Product image"
    />
    <div class="title">
      <p><b style="font-weight: bold;">{{ name }}</b></p>
      <p>
        {{ description }}
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
        tempor incididunt ut labore et dolore magna aliqua.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
        tempor incididunt ut labore et dolore magna aliqua.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
        tempor incididunt ut labore et dolore magna aliqua.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
        tempor incididunt ut labore et dolore magna aliqua.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
        tempor incididunt ut labore et dolore magna aliqua.
      </p>
    </div>
    <div>
      <img
        class="button-img"
        src="@/assets/button/minus_button.png"
        @click="subtractQuantity"
      />
      <input-component
        class="price-input"
        :value="quantity"
        @input="updateQuantity"
        type="number"
      />
      <img
        class="button-img"
        src="@/assets/button/plus_button.png"
        @click="addQuantity"
      />
    </div>
    <div>
      <p>{{ priceQuantity }}</p>
      <img
        class="button-img"
        src="@/assets/button/delete.png"
        @click="deleteQuantity"
      />
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
  props: {
    cardId: {
      type: Number,
      required: true,
    },
    cart: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      productId: this.cart.id,
      name: this.cart.name,
      price: this.cart.price,
      img: this.cart.img,
      description: this.cart.description,
      quantity: this.cart.quantity,
    };
  },
  computed: {
    priceQuantity() {
      return this.price * this.quantity;
    },
  },
  watch: {
    quantity(newValue) {
      if (newValue < 0) this.quantity = 0;
      this.$emit("updateQuantity", [this.cardId, newValue]);
    },
  },
  methods: {
    updateQuantity(e) {
      this.quantity = e;
    },
    addQuantity() {
      this.quantity += 1;
    },
    subtractQuantity() {
      this.quantity -= 1;
    },
    deleteQuantity() {
      this.$emit("delete", this.cardId);
    },
  },
};
</script>

<style scoped>
.cart-card-container {
  display: flex;
  justify-content: space-around;
  background-color: var(--main-white);
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
  max-height: 70%;
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
