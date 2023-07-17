<template>
    <label :for="name">{{ label }}</label>
    <input
      :id="id"
      :name="name"
      :type="type"
      :placeholder="placeholder"
      :class="$attrs.class"
      :style="styleObject"
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
    type: String,
    placeholder: String,
    label: String,
    styleObject: Object,
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
</style>
