<template>
    <label :for="name">{{ label }}</label>
    <input
      :id="id"
      :name="name"
      :type="type"
      :placeholder="placeholder"
      v-model="input"
      @change="$emit('input', input)"
    />
</template>

<script>
export default {
  name: "InputComponent",
  props: {
    id: String,
    name: String,
    type: {
      type: String,
      default: "text",},
    placeholder: String,
    label: String,
  },
  data() {
    return {
      input: "",
    };
  },
};
</script>

<style scoped>
label {
  align-self: start;
  margin-left: 3vw;
}

input {
  border-radius: 15px;
  height: 53px;
  width: 90%;
  text-align: center;
}
</style>
