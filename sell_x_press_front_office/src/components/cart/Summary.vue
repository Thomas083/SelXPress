<template>
  <div class="summary">
    <div>
        <p>Subtotal ({{ totalQuantity }} items) : <b>{{ totalPrice }}€</b></p>
        <p><small>Tax and delivery free price</small></p>
    </div>
    <button
      class="btn btn-primary go-to-payment-btn"
      v-on:click="goToPayement()"
    >
      Go to Payment
    </button>
  </div>
</template>

<script>
export default {
  name: "CartSummary",
  props: {
    cart: {
      type: Array,
      required: true,
    },
  },
  methods: {
    goToPayement() {
      console.dir(this.cart)
    }
  },
  computed: {
    totalQuantity() {
      return this.cart.reduce((acc, item) => acc + item.quantity, 0);
    },
    totalPrice() {
      return this.cart.reduce(
        (acc, item) => acc + item.product.price * item.quantity,
        0
      );
    },
  },
  watch: {
    cart(newValue, oldValue) {
      this.totalQuantity = newValue.reduce(
        (acc, item) => acc + item.quantity,
        0
      );
      this.totalPrice = newValue.reduce(
        (acc, item) => acc + item.price * item.quantity,
        0
      );
    },
  },
};
</script>

<style scoped>
.summary {
  background-color: var(--main-white);
  height: 250px;
  width: 250px;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  justify-content: center;
}

.go-to-payment-btn {
  margin-top: 1rem;
  margin-bottom: 1rem;
  max-width: 70%;
  border-radius: 15px;
}

b {
    font-weight: bold;
}

small {
    font-size: 0.8rem;
}

@media screen and (max-width: 430px) {
    .summary {
        height: 100px;
        width: 80vw;
    }

    .go-to-payment-btn {
        margin-top: 0;
        margin-bottom: 0;
        max-width: 60%;
    }

    p {
        margin: 0 !important;
        padding: 0 !important;
    }
}
</style>
