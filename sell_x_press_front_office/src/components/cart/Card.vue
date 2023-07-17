<template>
  <div class="container">
    <img src="" alt="product" />
    <p>Titre</p>
    <button>
      <img class="send-img" src="@/assets/button/minus_button.png" />
    </button>
    <input-component
        :style="styleObject"
    />
    <button>
      <img class="send-img" src="@/assets/button/plus_button.png" />
    </button>
    <div>
      <p>Price</p>
      <button>
        <img class="send-img" src="@/assets/button/delete.png" />
      </button>
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
  styleObject: {
    width: '90%',
    // 'border-radius': '15px',
  }
};
</script>

<style scoped>
.container {
  background-color: white;
  border-radius: 10px;
  width: 70vw;
  height: 20vh;
  align-items: center;
}

.inputcomponent {
    width: 90%;
    border-radius: 15px;
}
/* button {
    background-color: transparent;
    border: none;
} */

.send-img {
  width: 1.5rem;
}
</style>
