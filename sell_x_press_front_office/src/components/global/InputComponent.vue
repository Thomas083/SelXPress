<template>
    <label v-if="label!=''" :for="name">{{ label }}</label>
    <input
      :id="id"
      :name="name"
      :type="type"
      :placeholder="placeholder"
      :class="$attrs.class"
      :style="styleObject"
      :value="value"
      @change="$emit('input', returnValue($event.target.value))"
    />
</template>

<script>
export default {
  name: "InputComponent",
  props: {
    id: String,
    name: String,
    type: String,
    value: [String, Number],
    placeholder: String,
    label: {
      type: String,
      default: "",
    },
    styleObject: Object,
  },
  methods: {
    returnValue(e) {
      if (e.match(/.*[a-zA-Z].*/)) return e;
      else return parseInt(e);
    }
  },
};
</script>

<style scoped>
label {
  align-self: start;
  margin-left: 3vw;
}
</style>
