<template>
  <div :onChange="$emit('inputForm', formData)" class="form-container">
    <input-component
      label="Email address"
      id="input-mail"
      name="input-mail"
      type="text"
      placeholder="Enter your email"
      @input="updateData($event, 'email')"
    />
    <input-component
      label="Password"
      id="input-pwd"
      name="input-pwd"
      type="password"
      placeholder="Enter your password"
      @input="updateData($event, 'password')"
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
        email: "",
        password: "",
      },
    };
  },
  methods: {
    updateData(e, key) {
      this.formData = Object.assign(this.formData, { [key]: e });
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
