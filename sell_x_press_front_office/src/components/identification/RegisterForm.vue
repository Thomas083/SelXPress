<template>
  <div :onChange="$emit('inputForm', formData)" class="form-container">
    <input-component
      label="Username"
      id="input-uname"
      name="input-uname"
      type="text"
      placeholder="Enter your username"
      @input="updateData($event, 'username')"
    />
  </div>
</template>

<script>
import InputComponent from "@/components/global/InputComponent.vue";
export default {
  name: "RegisterForm",
  components: {
    InputComponent,
  },
  data() {
    return {
      formData: {
        username: "",
        email: "",
        password: "",
        cpassword: "",
      },
    };
  },
  methods: {
    updateData(e, key) {
      this.formData = {
        [key]: e,
      };
    },
  },
};
</script>

<style scoped>
.form-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 75%;
  width: 100%;
  border-radius: 10px;
  margin-top: 5vh;
}
</style>
