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
import { GET, POST } from '@/api/axios';
import { ENDPOINTS } from '@/api/endpoints';
import { createToast } from 'mosha-vue-toastify';

export default {
  name: "CartSummary",
  props: {
    cart: {
      type: Array,
      required: true,
    },
  },
  data() {
    return {
      sendOrder: {
        totalPrice: '',
        userId: '',
        orderProducts: []
      }
    }
  },
  methods: {
    goToPayement() {
      this.sendOrder.totalPrice = this.totalPrice
      this.sendOrder.userId = this.cart[0].userId
      for (let i = 0; i < this.cart.length; i++) {
        this.sendOrder.orderProducts.push({
          productId: this.cart[i].productId,
          quantity: this.cart[i].quantity
        })     
      };
      POST(ENDPOINTS.CREATE_ORDER, {userId: this.sendOrder.userId}, JSON.parse(localStorage.getItem('user')).token)
      .then(() => {
        this.$emit('refreshCart')
        createToast({ title: 'Order register succesfully', description: 'You sucessfully registered your order' }, { type: 'success', position: 'bottom-right' });
      })
      .catch(() => {
        createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
      })
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
